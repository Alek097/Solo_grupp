namespace API.Data.Repositories.Interfaces
{

	#region Using
	using API.Models;
	using Models;
	using System;
	using System.Threading.Tasks;
	#endregion
	public interface ICreateNewsRepository : IDisposable
	{
		Task<RepositoryResult<News, MoveTo>> CreateNews(CreateNews model);
	}
}
