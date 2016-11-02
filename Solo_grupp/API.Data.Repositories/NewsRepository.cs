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

		public async Task<ControllerResult<NewsModel>> GetNews(Guid id)
		{
			return await Task.Run<ControllerResult<NewsModel>>(() =>
			{
				ControllerResult<NewsModel> result = new ControllerResult<NewsModel>();

				News news = context.Get<News, Guid>(id);

				if (news == null)
				{
					result.IsSucces = false;
					result.Message = "Новость не найдена!";

					this.logger.WriteError(string.Format("Новость по id {0} не найдена.", id));
				}
				else
				{
					result.IsSucces = true;

					result.Value = new NewsModel();
					result.Value.Content = news.Content;
					result.Value.Id = news.Id;
					result.Value.Title = news.Title;

					result.Value.Author = new UserInformation();
					result.Value.Author.Id = news.User.Id;
					result.Value.Author.City = news.User.City;
					result.Value.Author.Country = news.User.Country;
					result.Value.Author.Email = news.User.Email;
					result.Value.Author.FirstName = news.User.FirstName;
					result.Value.Author.FullName = news.User.FullName;
					result.Value.Author.LastName = news.User.LastName;
					result.Value.Author.Patronymic = news.User.Patronymic;
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
