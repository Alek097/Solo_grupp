namespace API.Data.Repositories.Interfaces
{
	#region Using
	using Models;
	using System.Threading.Tasks;
	using System;
	#endregion
	public interface IUserRepository
	{
		Task<RepositoryResult> RegistartionAsync(NotActiveUser user);
		Task<RepositoryResult<User>> Activation(Guid id);
	}
}
