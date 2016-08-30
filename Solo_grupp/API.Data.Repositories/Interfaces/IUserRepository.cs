namespace API.Data.Repositories.Interfaces
{
	#region Using
	using Models;
	#endregion
	public interface IUserRepository
	{
		void Registartion(NotActiveUser user);
	}
}
