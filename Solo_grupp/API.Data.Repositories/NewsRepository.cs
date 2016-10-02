namespace API.Data.Repositories
{
	#region Using
	using API.Models;
	using System;
	using System.Threading.Tasks;
	using Interfaces;
	using Models;
	using Logging;
	#endregion
	public class NewsRepository : INewsRepository
	{
		private readonly IContext context;
		private readonly ILogger logger;
		public NewsRepository(IContext context, ILogger logger)
		{
			this.context = context;
			this.logger = logger;
		}

		public async Task<ControllerResult<News>> GetNews(Guid id)
		{
			return await new Task<ControllerResult<News>>(() =>
			{
				ControllerResult<News> result = new ControllerResult<News>();

				News news = context.Get<News, Guid>(id);

				if (news == null)
				{
					result.IsSucces = false;
					result.Message = "Новость не найдена!";

					this.logger.WriteError(string.Format("Новость по id {0} не найдена.", id));
				}
				else
				{
					news.Images = null;
					news.User = null;

					result.IsSucces = true;
					result.Value = news;
				}

				return result;
			});
		}

		public void Dispose()
		{
			this.context.Dispose();
		}
	}
}
