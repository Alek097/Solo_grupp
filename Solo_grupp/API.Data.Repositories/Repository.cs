using System.Net;
using System.Net.Mail;

namespace API.Data.Repositories
{
	public class Repository
	{
		public const string DNS = "http://localhost:11799";
		private const string email = "epamprojectChudo-pechka@yandex.ru";
		private const string emailPassword = "epamProject";
		public string MovedError(int httpCode, string message)
		{
			return string.Format("{0}/#/Error/{1}/{2}", DNS, httpCode, message);
		}
		public string MovedMessage(string message)
		{
			return string.Format("{0}/#/Message/{1}", DNS, message);
		}
		public string MovedHome()
		{
			return string.Format("{0}/#/Home", DNS);
		}
		public void SendMessage(string email, string subject, string message, bool isHtml)
		{
			MailAddress from = new MailAddress(Repository.email, "Solo-grupp");

			MailAddress to = new MailAddress(email);

			MailMessage m = new MailMessage(from, to);

			m.Subject = "Solo-grupp подтверждение аккаунта";

			m.Body = message;
			m.IsBodyHtml = isHtml;

			SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 25);

			smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtp.UseDefaultCredentials = false;
			smtp.EnableSsl = true;

			smtp.Credentials = new NetworkCredential(Repository.email, Repository.emailPassword);

			smtp.Send(m);
		}
	}
}
