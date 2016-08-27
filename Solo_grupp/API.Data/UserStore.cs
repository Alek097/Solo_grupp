namespace API.Data
{
	using System.Data.Entity;
	#region Using
	using Microsoft.AspNet.Identity.EntityFramework;
	using Models;
	#endregion
	public class UserStore : IdentityDbContext<User>
	{
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
	}
}
