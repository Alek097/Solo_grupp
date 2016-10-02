namespace API.Controllers
{
	#region Using
	using Data.Models;
	using System;
	using System.Threading.Tasks;
	using System.Web.Http;
	#endregion
	public class NewsController : ApiController
	{
		[HttpGet]
		public async Task<News> GetNews(Guid id)
		{

		}
	}
}
