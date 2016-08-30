namespace API.Data.Repositories.Interfaces
{
	#region Using
	using Models;
	using System.Threading.Tasks;
	#endregion
	public interface IUserRepository
	{
		Task RegistartionAsync(NotActiveUser user);
	}
}
