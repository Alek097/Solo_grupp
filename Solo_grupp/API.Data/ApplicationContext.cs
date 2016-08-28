namespace API.Data
{
	using System;
	using System.Data.Entity;
	#region Using
	using Microsoft.AspNet.Identity.EntityFramework;
	using Models;
	#endregion
	public class ApplicationContext : IdentityDbContext<User>, IContext
	{
		public ApplicationContext()
			: base("solo_grupp_users")
		{

		}
		public DbSet<News> News { get; set; }
		public DbSet<Resolution> Permission { get; set; }
		public DbSet<Image> Images { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.HasMany(user => user.Permission)
				.WithOptional(resolution => resolution.User);

			modelBuilder.Entity<User>()
				.HasMany(user => user.News)
				.WithOptional(news => news.User);

			modelBuilder.Entity<News>()
				.HasMany(news => news.Images)
				.WithOptional(images => images.News);
		}

		public void Add<TEntity>(TEntity entity)
			where TEntity : class
		{
			this.Entry(entity).State = EntityState.Added;
		}

		public void Delete<TEntity>(TEntity entity)
			where TEntity : class
		{
			this.Entry(entity).State = EntityState.Deleted;
		}

		public void Update<TEntity>(TEntity entity)
			where TEntity : class
		{
			this.Entry(entity).State = EntityState.Modified;
		}

		public static ApplicationContext Create()
		{
			return new ApplicationContext();
		}
	}
}
