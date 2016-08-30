namespace API.Data.Repositories
{
	#region Using
	using System;
	using Models;
	using Interfaces;
	using Logging;
	using System.Net.Mail;
	using System.Threading.Tasks;
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

				smtp.Credentials = new System.Net.NetworkCredential(email, password);

				await smtp.SendMailAsync(m);
			}
			catch(Exception ex)
			{
				logger.WriteError(ex, string.Format("Ошибка при отправке сообщения на {0} с {1}.", user.Email, email));
			}
		}
	}
}
