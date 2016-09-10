namespace API.Data.Repositories.Interfaces
{
	#region Using
	using Models;
	using System.Threading.Tasks;
	using System;
	using API.Models;
	using System.Net.Http;
	#endregion
	public interface IUserRepository
	{
		Task<RepositoryResult<MoveTo>> RegistartionAsync(NotActiveUser user);
		Task<RepositoryResult<User, HttpResponseMessage>> Activation(Guid id);
		Task<RepositoryResult<User, MoveTo>> SignIn(SignIn model);
		Task<RepositoryResult<MoveTo>> Replace(string email);
		Task<RepositoryResult<MoveTo>> Replace(Replace model);
		Task<RepositoryResult<HttpResponseMessage>> CancelReplace(Guid replaceCode);
	}
}
