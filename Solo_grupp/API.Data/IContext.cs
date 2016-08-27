namespace API.Data
{
	#region Using
	using Models;
	using System.Data.Entity;
	using System.Threading.Tasks;
	#endregion
	public interface IContext
	{
		DbSet<News> News { get; set; }
		DbSet<Resolution> Permission { get; set; }
		DbSet<Image> Images { get; set; }
		Task<int> SaveChangesAsync();
		int SaveChanges();
		void Add<TEntity>(TEntity entity)
			where TEntity : class;
		void Delete<TEntity>(TEntity entity)
			where TEntity : class;
		void Update<TEntity>(TEntity entity)
			where TEntity : class;
	}
}
