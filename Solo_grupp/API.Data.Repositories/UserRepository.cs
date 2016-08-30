namespace API.Data.Repositories
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
	public class UserRepository : IUserRepository
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

				response.Headers.Location = new Uri("http://localhost:11799/#/error?httpCode=404&message=Пользователь не найден");

				result.Responce = response;
			}
			else
			{
				User newUser = new User(notActiveUser);
				this.context.Add(newUser);
				this.context.Delete(notActiveUser);
				int changes = await this.context.SaveChangesAsync();

				if (changes == 0)
				{
					HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
					response.Headers.Location = new Uri("http://localhost:11799/#/error?httpCode=500&message=Ошибка на сервере");

					result.Responce = response;
				}
				else
				{
					this.logger.WriteInformation(string.Format("Пользователь с id = {0} и ФИО = {1} успешно активировался, теперь id = {3}",
						notActiveUser.Id,
						newUser.FullName,
						newUser.Id));

					HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
					response.Headers.Location = new Uri("http://localhost:11799/#/SignIn");

					result.Responce = response;
					result.Value = newUser;
				}
			}
			return result;
		}

		public async Task RegistartionAsync(NotActiveUser user)
		{
			context.Add(user);
			await context.SaveChangesAsync();

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
			}
			catch (Exception ex)
			{
				this.logger.WriteError(ex, string.Format("Ошибка при отправке сообщения на {0} с {1}.", user.Email, email));
			}
		}
	}
}
