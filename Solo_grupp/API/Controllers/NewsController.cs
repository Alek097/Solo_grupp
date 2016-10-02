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
		public async Task<ControllerResult<News>> GetNews(Guid id)
		{
			return await this.repository.GetNews(id);
		}

		public new void Dispose()
		{
			this.repository.Dispose();
			base.Dispose();
		}
	}
}
