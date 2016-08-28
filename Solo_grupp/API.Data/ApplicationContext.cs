namespace API.Data
{
	#region Using
	using System;
	using System.Data.Entity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Models;
	using System.Data;
	using Logging;
	using System.Threading.Tasks;
	#endregion
	public class ApplicationContext : IdentityDbContext<User>, IContext
	{
		private ILogger logger;
		public ApplicationContext(ILogger logger)
			: base("solo_grupp_users")
		{
			this.logger = logger;
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

		public new int SaveChanges()
		{
			try
			{
				int changes = base.SaveChanges();
				this.logger.WriteInformation(string.Format("Произошли изменения в БД (Изменений - {0})", changes));
				return changes;
			}
			catch (DataException ex)
			{
				this.logger.WriteFatal(ex, "Произошла фатальная ошибка при сохранении изменений в БД, дальнейшая работа невозможна");
			}
			catch(Exception ex)
			{
				this.logger.WriteFatal(ex, "Произошла ошибка при сохранении изменений в БД");
			}

			return 0;
		}

		public new async Task<int> SaveChangesAsync()
		{
			try
			{
				int changes = await base.SaveChangesAsync();
				this.logger.WriteInformation(string.Format("Произошли изменения в БД (Изменений - {0})", changes));
				return changes;
			}
			catch (DataException ex)
			{
				this.logger.WriteFatal(ex, "Произошла фатальная ошибка при сохранении изменений в БД");
			}
			catch (Exception ex)
			{
				this.logger.WriteFatal(ex, "Произошла ошибка при сохранении изменений в БД");
			}

			return 0;
		}
	}
}
