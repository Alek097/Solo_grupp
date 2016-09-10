namespace API.Data
{
	#region Using
	using Logging;
	using System.Data.Entity.Infrastructure;
	#endregion
	public class MigrationsContextFactory : IDbContextFactory<ApplicationContext>
	{
		public ApplicationContext Create()
		{
			return new ApplicationContext(new Logger());
		}
	}
}
