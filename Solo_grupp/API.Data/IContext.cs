namespace API.Data
{
	#region Using
	using Models;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Threading.Tasks;
	#endregion
	public interface IContext
	{
		DbSet<News> News { get; set; }
		DbSet<Resolution> Permission { get; set; }
		DbSet<Image> Images { get; set; }
		DbSet<NotActiveUser> NotActiveUsers { get; set; }
		DbSet<Salt> Salts { get; set; }
		Task<int> SaveChangesAsync();
		int SaveChanges();
		void Add<TEntity>(TEntity entity)
			where TEntity : class;
		void Delete<TEntity>(TEntity entity)
			where TEntity : class;
		void Update<TEntity>(TEntity entity)
			where TEntity : class;
		ICollection<TEntity> GetAll<TEntity>()
			where TEntity : class;
		TEntity Get<TEntity, TId>(TId idEntity)
			where TEntity : class;
	}
}
