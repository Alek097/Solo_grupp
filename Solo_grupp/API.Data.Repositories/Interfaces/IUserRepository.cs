namespace API.Data.Repositories.Interfaces
{
	#region Using
	using Models;
	using System.Threading.Tasks;
	using System;
	using API.Models;
	using System.Net.Http;
	#endregion
	public interface IUserRepository : IDisposable
	{
		Task<RepositoryResult<ControllerResult>> RegistartionAsync(NotActiveUser user);
		Task<RepositoryResult<User, HttpResponseMessage>> Activation(Guid id);
		Task<RepositoryResult<User, ControllerResult>> SignIn(SignIn model);
		Task<RepositoryResult<ControllerResult>> Replace(string email);
		Task<RepositoryResult<ControllerResult>> Replace(Replace model);
		Task<RepositoryResult<HttpResponseMessage>> CancelReplace(Guid replaceCode);
	}
}
