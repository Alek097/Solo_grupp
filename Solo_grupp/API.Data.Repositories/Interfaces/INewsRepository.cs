namespace API.Data.Repositories.Interfaces
{
	#region Using
	using API.Models;
	using Models;
	using System;
	using System.Threading.Tasks;
	#endregion
	public interface INewsRepository : IDisposable
	{
		Task<ControllerResult<NewsModel>> GetNews(Guid id);
	}
}
