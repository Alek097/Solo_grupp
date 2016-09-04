namespace API.Data.Repositories
{
	public class Repository
	{
		public const string DNS = "http://localhost:11799";
		public string MovedError(int httpCode, string message)
		{
			return string.Format("{0}/#/Error/{1}/{2}", DNS, httpCode, message);
		}
		public string MovedMessage(string message)
		{
			return string.Format("{0}/#/Message/{1}", DNS, message);
		}
	}
}
