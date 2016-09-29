namespace API.Data.Repositories
{
	#region Using
	using System;
	using Models;
	using Interfaces;
	using Logging;
	using System.Threading.Tasks;
	using System.Net.Http;
	using System.Net;
	using System.Linq;
	using API.Models;
	#endregion
	public class UserRepository : Repository, IUserRepository
	{
		private readonly IContext context;
		private readonly ILogger logger;
		public UserRepository(IContext context, ILogger logger)
		{
			this.context = context;
			this.logger = logger;
		}

		public async Task<RepositoryResult<User, HttpResponseMessage>> Activation(Guid id)
		{
			RepositoryResult<User, HttpResponseMessage> result = new RepositoryResult<User, HttpResponseMessage>();

			NotActiveUser notActiveUser = this.context.Get<NotActiveUser, Guid>(id);

			if (notActiveUser == null)
			{
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Moved);

				response.Headers.Location = new Uri(base.MovedError(404, "Пользователь не найден"));

				result.Responce = response;
				result.ResultType = RepositoryResultType.Bad;

				logger.WriteError(string.Format("Не активированный пользователь с id = {0} не найден"));
			}
			else
			{
				User newUser = new User(notActiveUser);
				this.context.Delete(notActiveUser);
				await this.context.SaveChangesAsync();


				this.logger.WriteInformation(string.Format("Пользователь с id = {0} и ФИО = {1} успешно активировался, теперь id = {2}",
					notActiveUser.Id,
					newUser.FullName,
					newUser.Id));

				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Moved);
				response.Headers.Location = new Uri(string.Format("{0}/#/SignIn", DNS));

				result.Responce = response;
				result.Value = newUser;
				result.ResultType = RepositoryResultType.OK;
			}
			return result;
		}

		public async Task<RepositoryResult<ControllerResult>> RegistartionAsync(NotActiveUser user)
		{
			RepositoryResult<ControllerResult> result = new RepositoryResult<ControllerResult>();

			NotActiveUser isHaveUser = context.GetAll<NotActiveUser>().FirstOrDefault((u) => u.Email == user.Email);

			if (isHaveUser != null)
			{

				result.ResultType = RepositoryResultType.Bad;

				result.Responce = new ControllerResult()
				{
					IsSucces = true,
					Message = this.MovedSignUpError(string.Format("Почта {0} уже занята.", user.Email))
				};

				return result;
			}

			try
			{
				string href = string.Format("{0}/api/User/Activation?id={1}", DNS, user.Id);

				base.SendMessage(
					user.Email,
					"Solo-grupp подтверждение аккаунта",
					string.Format("<a href=\"{0}\">{0}</a>", href),
					true);

				this.logger.WriteInformation(string.Format("Зарегестрировался новый пользователь id = {0}. Письмо подтверждения отправлено на {1}.", user.Id, user.Email));

				result.ResultType = RepositoryResultType.OK;

				result.Responce = new ControllerResult()
				{
					IsSucces = true,
					Message = base.MovedMessage(string.Format("Письмо с подтверждение отправлено на {0}", user.Email))
				};

				context.Add(user);

				int changes = await context.SaveChangesAsync();

				if (changes == 0)
				{
					result.ResultType = RepositoryResultType.Bad;

					result.Responce = new ControllerResult()
					{
						IsSucces = true,
						Message = base.MovedError(500, "Ошибка на серевере")
					};

					return result;
				}
			}
			catch (Exception ex)
			{
				this.logger.WriteError(ex, string.Format("Ошибка при отправке сообщения на {0}.", user.Email));

				result.ResultType = RepositoryResultType.Bad;

				result.Responce = new ControllerResult()
				{
					IsSucces = true,
					Message = base.MovedError(500, string.Format("Не удалось отправить письмо на {0}", user.Email))
				};
			}

			return result;
		}

		public async Task<RepositoryResult<User, ControllerResult>> SignIn(SignIn model)
		{
			return await Task.Run<RepositoryResult<User, ControllerResult>>(() =>
				{
					RepositoryResult<User, ControllerResult> result = new RepositoryResult<User, ControllerResult>();

					User usr = this.context.GetAll<User>().FirstOrDefault((u) => u.Email == model.Email);

					if (usr == null)
					{
						result.ResultType = RepositoryResultType.Bad;
						result.Responce = new ControllerResult()
						{
							IsSucces = true,
							Message = this.MovedSignInError("Неверный логин или пароль")
						};

						logger.WriteError(string.Format("Пользователь с почтой {0} не найден.", model.Email));
					}
					else
					{
						string hashPassword = User.HashPassword(model.Password, usr.Salt);

						if (usr.PasswordHash == hashPassword)
						{
							result.ResultType = RepositoryResultType.OK;
							result.Value = usr;
							result.Responce = new ControllerResult()
							{
								IsSucces = true,
								Message = base.MovedHome()
							};

							logger.WriteInformation(string.Format("Пользователь с почтой {0} успешно вошёл в систему.", model.Email));
						}
						else
						{
							result.ResultType = RepositoryResultType.Bad;
							result.Responce = new ControllerResult()
							{
								IsSucces = true,
								Message = this.MovedSignInError("Неверный логин или пароль")
							};

							logger.WriteError("Хэши паролей не совпадают");
						}
					}

					return result;
				});
		}

		private string MovedSignUpError(string message)
		{
			return string.Format("{0}/#/SignUp/{1}", DNS, message);
		}
		private string MovedSignInError(string message)
		{
			return string.Format("{0}/#/SignIn/{1}", DNS, message);
		}

		private string MovedReplaceError(string message)
		{
			return string.Format("{0}/#/Replace/{1}", DNS, message);
		}

		public async Task<RepositoryResult<ControllerResult>> Replace(string email)
		{
			User user = this.context.GetAll<User>().FirstOrDefault(u => u.Email == email);

			RepositoryResult<ControllerResult> result = new RepositoryResult<ControllerResult>();

			if (user == null)
			{
				result.ResultType = RepositoryResultType.Bad;
				result.Responce = new ControllerResult()
				{
					IsSucces = true,
					Message = this.MovedReplaceError(string.Format("Пользователь с почтой {0} не найден.", email))
				};

			}
			else
			{
				Guid replaceCode = Guid.NewGuid();

				user.ReplaceCode = replaceCode;

				this.context.Update(user);

				int changes = await this.context.SaveChangesAsync();

				if (changes == 0)
				{
					result.ResultType = RepositoryResultType.Bad;
					result.Responce = new ControllerResult()
					{
						IsSucces = true,
						Message = this.MovedError(500, "Ошибка на сервере.")
					};

					logger.WriteError("Данные не были сохранены.");

					return result;
				}

				try
				{
					base.SendMessage(
						user.Email,
						"Solo-grupp смена пароля.",

						string.Format("<span>Ваш код подтверждения: {0}</span><br/><span>Если вы не запрашивали смены пароля, перейдите по ссылке : <a href=\"{1}\">{1}</a></span>",
							replaceCode,
							string.Format("{0}/api/user/cancelReplace?replaceCode={1}", DNS, replaceCode)),

						true
						);
				}
				catch (Exception ex)
				{
					logger.WriteError(ex, string.Format("Не удалось отправить сообщение с кодом подтверждения на {0}.", email));

					result.ResultType = RepositoryResultType.Bad;
					result.Responce = new ControllerResult()
					{
						IsSucces = true,
						Message = this.MovedError(500, "Ошибка на сервере.")
					};
				}


				result.ResultType = RepositoryResultType.OK;
				result.Responce = new ControllerResult()
				{
					IsSucces = false
				};
			}

			logger.WriteInformation(string.Format("Отправлено письмо с кодом подтверждения на {0}", email));

			return result;
		}

		public async Task<RepositoryResult<HttpResponseMessage>> CancelReplace(Guid replaceCode)
		{
			RepositoryResult<HttpResponseMessage> result = new RepositoryResult<HttpResponseMessage>();
			result.Responce = new HttpResponseMessage(HttpStatusCode.Moved);
			result.Responce.Headers.Location = new Uri(string.Format("{0}/#/Home", DNS));

			User user = this.context.GetAll<User>().FirstOrDefault(u => u.ReplaceCode == replaceCode);

			if (user == null)
			{
				result.ResultType = RepositoryResultType.Bad;

				logger.WriteError(string.Format("Пользователь с кодом подтверждения {0} не найден.", replaceCode));
			}
			else
			{
				result.ResultType = RepositoryResultType.OK;

				user.ReplaceCode = Guid.Empty;

				await this.context.SaveChangesAsync();


				logger.WriteInformation(string.Format("Произведена отмена смены пароля пользователя с кодом подтверждения {0}", replaceCode));
			}

			return result;
		}

		public async Task<RepositoryResult<ControllerResult>> Replace(Replace model)
		{
			User user = this.context.GetAll<User>().FirstOrDefault(u => u.ReplaceCode == model.ReplaceCode);

			RepositoryResult<ControllerResult> result = new RepositoryResult<ControllerResult>();

			if (user == null || user.ReplaceCode == Guid.Empty)
			{
				logger.WriteError(string.Format("Пользователь с кодом подтверждения {0} не найден.", model.ReplaceCode));

				result.ResultType = RepositoryResultType.Bad;
				result.Responce = new ControllerResult()
				{
					IsSucces = true,
					Message = this.MovedReplaceError("Код не найден, проверьте правильность введённого кода. Если всё правильно попробуйте повторить операцию. Если ошибка не исчезает обратитесь в техническую поддержку.")
				};
			}
			else
			{
				Salt salt = user.Salt;

				this.context.Delete(salt);

				user.Salt = new Salt();
				user.PasswordHash = User.HashPassword(model.Password, user.Salt);
				user.ReplaceCode = Guid.Empty;

				int changes = await this.context.SaveChangesAsync();

				if (changes == 0)
				{
					logger.WriteError("Данные не были сохранены.");

					result.ResultType = RepositoryResultType.Bad;
					result.Responce = new ControllerResult()
					{
						IsSucces = true,
						Message = base.MovedError(500, "Ошибка на сервере.")
					};
				}
				else
				{
					logger.WriteInformation("Пароль успешно изменён.");

					result.ResultType = RepositoryResultType.OK;
					result.Responce = new ControllerResult()
					{
						IsSucces = true,
						Message = "/#/SignIn"
					};
				}
			}

			return result;
		}

		public void Dispose()
		{
			this.context.Dispose();
		}
	}
}
