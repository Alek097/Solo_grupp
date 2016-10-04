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
	using System.Collections.Generic;
	using System.Linq;
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
		public DbSet<NotActiveUser> NotActiveUsers { get; set; }
		public DbSet<Salt> Salts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.HasMany(user => user.Permission)
				.WithOptional(resolution => resolution.User);

			modelBuilder.Entity<User>()
				.HasMany(user => user.News)
				.WithOptional(news => news.User);
			modelBuilder.Entity<User>()
	.			HasMany(user => user.Comments)
				.WithOptional(comment => comment.Author);

			modelBuilder.Entity<User>()
				.HasRequired(user => user.Salt)
				.WithOptional(salt => salt.User);

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
			catch (Exception ex)
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
				this.logger.WriteFatal(ex, "Произошла фатальная ошибка при сохранении изменений в БД, дальнейшая работа невозможна");
			}
			catch (Exception ex)
			{
				this.logger.WriteFatal(ex, "Произошла ошибка при сохранении изменений в БД");
			}

			return 0;
		}

		public ICollection<TEntity> GetAll<TEntity>()
			where TEntity : class
		{
			Type typeEntity = typeof(TEntity);

			if (typeEntity == typeof(User))
			{
				return this.Users
					.Include(user => user.Permission)
					.Include(user => user.News)
					.Include(user => user.Salt)
					.ToList() as ICollection<TEntity>;
			}
			else if (typeEntity == typeof(NotActiveUser))
			{
				return this.NotActiveUsers.ToList() as ICollection<TEntity>;
			}
			else if (typeEntity == typeof(Salt))
			{
				return this.Salts
					.Include(salt => salt.User)
					.ToList() as ICollection<TEntity>;
			}
			else if (typeEntity == typeof(News))
			{
				return this.News
					.Include(news => news.User)
					.Include(news => news.Images)
					.ToList() as ICollection<TEntity>;
			}
			else if (typeEntity == typeof(Image))
			{
				return this.Images
					.Include(image => image.News)
					.ToList() as ICollection<TEntity>;
			}
			else if (typeEntity == typeof(Resolution))
			{
				return this.Permission
					.Include(resolution => resolution.User)
					.ToList() as ICollection<TEntity>;
			}
			else
			{
				throw new ArgumentException(string.Format("Entity with type {0} not a found.", typeEntity));
			}
		}

		public TEntity Get<TEntity, TId>(TId idEntity)
			where TEntity : class
		{
			Type typeEntity = typeof(TEntity);
			Type typeId = typeof(TId);

			if (typeEntity == typeof(User))
			{
				if (typeId == typeof(string))
				{
					string id = idEntity as string;

					User user = this.GetAll<User>().FirstOrDefault(u => u.Id == id);

					if (user == null)
					{
						this.logger.WriteError(string.Format("Активиованный пользователь с id = {0} не найден", id));
						return null;
					}
					else
					{
						return user as TEntity;
					}
				}
				else
				{
					throw new ArgumentException(string.Format("Type {0} of id for {1} is not valid. Type of Id {2} for {1} is valid"
						, typeId
						, typeof(User)
						, typeof(string)));
				}
			}
			else if (typeEntity == typeof(NotActiveUser))
			{
				if (typeId == typeof(Guid))
				{
					Guid id = (Guid)(object)idEntity;

					NotActiveUser user = this.GetAll<NotActiveUser>().FirstOrDefault(u => u.Id == id);

					if (user == null)
					{
						this.logger.WriteError(string.Format("Не активиованный пользователь с id = {0} не найден", id));
						return null;
					}
					else
					{
						return user as TEntity;
					}
				}
				else
				{
					throw new ArgumentException(string.Format("Type {0} of id for {1} is not valid. Type of Id {2} for {1} is valid"
						, typeId
						, typeof(NotActiveUser)
						, typeof(Guid)));
				}
			}
			else if (typeEntity == typeof(Salt))
			{
				if (typeId == typeof(Guid))
				{
					Guid id = (Guid)(object)idEntity;

					Salt salt = this.GetAll<Salt>().FirstOrDefault(s => s.Id == id);

					if (salt == null)
					{
						this.logger.WriteError(string.Format("Соль пароля с id = {0} не найден", id));
						return null;
					}
					else
					{
						return salt as TEntity;
					}
				}
				else
				{
					throw new ArgumentException(string.Format("Type {0} of id for {1} is not valid. Type of Id {2} for {1} is valid"
						, typeId
						, typeof(Salt)
						, typeof(Guid)));
				}
			}
			else if (typeEntity == typeof(News))
			{
				if (typeId == typeof(Guid))
				{
					Guid id = (Guid)(object)idEntity;

					News news = this.GetAll<News>().FirstOrDefault(n => n.Id == id);

					if (news == null)
					{
						this.logger.WriteError(string.Format("Новость с id = {0} не найден", id));
						return null;
					}
					else
					{
						return news as TEntity;
					}
				}
				else
				{
					throw new ArgumentException(string.Format("Type {0} of id for {1} is not valid. Type of Id {2} for {1} is valid"
						, typeId
						, typeof(News)
						, typeof(Guid)));
				}
			}
			else if (typeEntity == typeof(Image))
			{
				if (typeId == typeof(Guid))
				{
					Guid id = (Guid)(object)idEntity;

					Image image = this.GetAll<Image>().FirstOrDefault(i => i.Id == id);

					if (image == null)
					{
						this.logger.WriteError(string.Format("Изображение с id = {0} не найден", id));
						return null;
					}
					else
					{
						return image as TEntity;
					}
				}
				else
				{
					throw new ArgumentException(string.Format("Type {0} of id for {1} is not valid. Type of Id {2} for {1} is valid"
						, typeId
						, typeof(Image)
						, typeof(Guid)));
				}
			}
			else if (typeEntity == typeof(Resolution))
			{
				if (typeId == typeof(Guid))
				{
					Guid id = (Guid)(object)idEntity;

					Resolution resolution = this.GetAll<Resolution>().FirstOrDefault(r => r.Id == id);

					if (resolution == null)
					{
						this.logger.WriteError(string.Format("Разрешение с id = {0} не найден", id));
						return null;
					}
					else
					{
						return resolution as TEntity;
					}
				}
				else
				{
					throw new ArgumentException(string.Format("Type {0} of id for {1} is not valid. Type of Id {2} for {1} is valid"
						, typeId
						, typeof(Resolution)
						, typeof(Guid)));
				}
			}
			else
			{
				throw new ArgumentException(string.Format("Entity with type {0} not a found.", typeEntity));
			}
		}
	}
}
