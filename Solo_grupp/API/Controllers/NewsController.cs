namespace API.Controllers
{
	#region Using
	using Data.Models;
	using Data.Repositories.Interfaces;
	using Models;
	using System;
	using System.Threading.Tasks;
	using System.Web.Http;
	#endregion
	public class NewsController : ApiController
	{
		private readonly INewsRepository repository;
		public NewsController(INewsRepository repository)
		{
			this.repository = repository;
		}
		[HttpGet]
		public async Task<ControllerResult<NewsModel>> GetNews(Guid id)
		{
			ControllerResult<NewsModel> result = await this.repository.GetNews(id);

			return result;
		}

		public new void Dispose()
		{
			this.repository.Dispose();
			base.Dispose();
		}
	}
}
