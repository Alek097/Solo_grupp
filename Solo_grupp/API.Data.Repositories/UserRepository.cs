﻿namespace API.Data.Repositories
{
	#region Using
	using System;
	using Models;
	using Interfaces;
	using Logging;
	using System.Net.Mail;
	using System.Threading.Tasks;
	using System.Net.Http;
	using System.Net;
	#endregion
	public class UserRepository : Repository, IUserRepository
	{
		private readonly IContext context;
		private readonly ILogger logger;
		public UserRepository(IContext context, ILogger logger)
		{
			this.context = context;
			this.logger = logger;
		}

		public async Task<RepositoryResult<User>> Activation(Guid id)
		{
			RepositoryResult<User> result = new RepositoryResult<User>();

			NotActiveUser notActiveUser = this.context.Get<NotActiveUser, Guid>(id);

			//TODO: Исправить
			if (notActiveUser == null)
			{
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Moved);

				response.Headers.Location = new Uri(base.MovedError(404, "Пользователь не найден"));

				result.Responce = response;
				result.ResultType = RepositoryResultType.Bad;

				logger.WriteError(string.Format("Не активированный пользователь с id = {0} не найден"));
			}
			else
			{
				User newUser = new User(notActiveUser);
				this.context.Delete(notActiveUser);
				await this.context.SaveChangesAsync();


				this.logger.WriteInformation(string.Format("Пользователь с id = {0} и ФИО = {1} успешно активировался, теперь id = {3}",
					notActiveUser.Id,
					newUser.FullName,
					newUser.Id));

				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Moved);
				response.Headers.Location = new Uri(string.Format("{0}/#/SignIn", DNS));

				result.Responce = response;
				result.Value = newUser;
				result.ResultType = RepositoryResultType.OK;
			}
			return result;
		}

		public async Task<RepositoryResult> RegistartionAsync(NotActiveUser user)
		{
			RepositoryResult result = new RepositoryResult();

			context.Add(user);
			int changes = await context.SaveChangesAsync();

			if(changes == 0)
			{
				result.Responce = new HttpResponseMessage(HttpStatusCode.Moved);
				result.Responce.Headers.Location = new Uri(base.MovedError(500, "Ошибка на серевере"));
				result.ResultType = RepositoryResultType.Bad;

				return result;
			}

			string email = "epamprojectChudo-pechka@yandex.ru";
			string password = "epamProject";

			try
			{
				MailAddress from = new MailAddress(email, "Chudo-Pechka");

				MailAddress to = new MailAddress(user.Email);

				MailMessage m = new MailMessage(from, to);

				m.Subject = "Solo-grupp подтверждение аккаунта";

				m.Body = string.Format("http://localhost:11799/api/User/Activation?id={0}", user.Id);
				m.IsBodyHtml = true;

				SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 25);

				smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtp.UseDefaultCredentials = false;
				smtp.EnableSsl = true;

				smtp.Credentials = new NetworkCredential(email, password);

				await smtp.SendMailAsync(m);

				this.logger.WriteInformation(string.Format("Зарегестрировался новый пользователь id = {0}. Письмо подтверждения отправлено на {1}.", user.Id, user.Email));

				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Moved);

				response.Headers.Location = new Uri(base.MovedMessage(string.Format("Письмо с подтверждение отправлено на {0}", user.Email)));

				result.Responce = response;

				result.ResultType = RepositoryResultType.OK;
			}
			catch (Exception ex)
			{
				this.logger.WriteError(ex, string.Format("Ошибка при отправке сообщения на {0} с {1}.", user.Email, email));

				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Moved);

				response.Headers.Location = new Uri(base.MovedError(500, string.Format("Не удалось отправить письмо на {0}", user.Email)));

				result.Responce = response;

				result.ResultType = RepositoryResultType.Bad;
			}

			return result;
		}
	}
}
