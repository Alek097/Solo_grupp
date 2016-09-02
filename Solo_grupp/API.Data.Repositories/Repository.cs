namespace API.Data.Repositories
{
	public class Repository
	{
		protected const string DNS = "";
		public string MovedError(int httpCode, string message)
		{
			return string.Format("{0}/#/Error/{1}/{2}", DNS, httpCode, message);
		}
	}
}
