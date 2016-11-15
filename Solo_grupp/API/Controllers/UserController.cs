namespace API.Controllers
{
	#region Using
	using System;
	using System.Net.Http;
	using System.Web.Http;
	using Data;
	using System.Threading.Tasks;
	using Models;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.Owin;
	using Data.Models;
	using Data.Repositories.Interfaces;
	using Data.Repositories;
	using System.Web.Http.ModelBinding;
	using Microsoft.Owin.Security;
	using System.Security.Claims;
	using Logging;
	#endregion
	public class UserController : ApiController
	{
		private readonly IUserRepository repository;
		private readonly ILogger logger;
		private ApplicationUserManager UserManager
		{
			get
			{
				return this.ControllerContext.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
		}
		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return this.ControllerContext.Request.GetOwinContext().Authentication;
			}
		}
		public UserController(IUserRepository repository, ILogger logger)
		{
			this.repository = repository;
			this.logger = logger;
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<ControllerResult> SignUp(SignUp model)
		{
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Empty;

				foreach (ModelState modelState in ModelState.Values)
				{
					foreach (ModelError error in modelState.Errors)
					{
						errorMessage = string.Format("{0}\n{1}", errorMessage, error.ErrorMessage);
					}
				}

				ControllerResult responce = new ControllerResult()
				{
					IsSucces = false,
					Message = errorMessage
				};

				return responce;
			}

			NotActiveUser user = new NotActiveUser(model);
			RepositoryResult<ControllerResult> result = await this.repository.RegistartionAsync(user);

			return result.Responce;
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<ControllerResult> SignIn(SignIn model)
		{
			RepositoryResult<ControllerResult> result = new RepositoryResult<ControllerResult>();

			RepositoryResult<User, ControllerResult> repoResult = await this.repository.SignIn(model);

			result.Responce = repoResult.Responce;

			if (repoResult.Value == null)
			{
				return result.Responce;
			}
			else
			{
				ClaimsIdentity claim = await UserManager.CreateIdentityAsync(repoResult.Value,
									DefaultAuthenticationTypes.ApplicationCookie);
				AuthenticationManager.SignOut();
				AuthenticationManager.SignIn(new AuthenticationProperties
				{
					IsPersistent = true
				}, claim);

				return result.Responce;
			}
		}
		[AllowAnonymous]
		[HttpGet]
		public async Task<HttpResponseMessage> Activation(Guid id)
		{
			RepositoryResult<User, HttpResponseMessage> result = await this.repository.Activation(id);

			if (result.ResultType == RepositoryResultType.OK)
			{
				IdentityResult identityResult = await this.UserManager.CreateAsync(result.Value, result.Value.PasswordHash);
			}

			return result.Responce;
		}
		[AllowAnonymous]
		[HttpGet]
		public async Task<UserInformation> Authentification()
		{
			return await Task.Run<UserInformation>(() =>
			{
				User currentUser = this.UserManager.FindById(this.User.Identity.GetUserId());

				if (currentUser == null)
				{
					return null;
				}
				else
				{
					return new UserInformation
					{
						Id = currentUser.Id,
						Country = currentUser.Country,
						City = currentUser.City,
						Email = currentUser.Email,
						FirstName = currentUser.FirstName,
						FullName = currentUser.FullName,
						LastName = currentUser.LastName,
						Patronymic = currentUser.Patronymic
					};
				}
			});
		}
		[AllowAnonymous]
		[HttpGet]
		public async Task SignOut()
		{
			User currentUser = this.UserManager.FindById(this.User.Identity.GetUserId());

			logger.WriteInformation(string.Format("Пользователь с почтой {0} вышел из системы.", currentUser.Email));

			await Task.Run(() =>
			{
				AuthenticationManager.SignOut();
			});
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<ControllerResult> Replace(Replace model)
		{
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Empty; ;

				foreach (ModelState modelState in ModelState.Values)
				{
					foreach (ModelError error in modelState.Errors)
					{
						errorMessage = string.Format("{0}\n{1}");
					}
				}

				ControllerResult responce = new ControllerResult()
				{
					IsSucces = true,
					Message = string.Format("{0}/#/Replace/{1}", Repository.DNS, errorMessage)
				};

				RepositoryResult<ControllerResult> result = new RepositoryResult<ControllerResult>();
				result.Responce = responce;
				result.ResultType = RepositoryResultType.Bad;

				return result.Responce;
			}

			return (await this.repository.Replace(model)).Responce;

		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<ControllerResult> Replace([FromUri]string email)
		{
			logger.WriteInformation(string.Format("Зпрос на смену пароля пользователя с почтой {0}", email));
			return (await this.repository.Replace(email)).Responce;
		}
		[AllowAnonymous]
		[HttpGet]
		public async Task<HttpResponseMessage> CancelReplace(Guid replaceCode)
		{
			logger.WriteInformation(string.Format("Отмена запроса на смена пароля пользователя с кодом подтверждения {0}", replaceCode));

			RepositoryResult<HttpResponseMessage> result = await this.repository.CancelReplace(replaceCode);

			return result.Responce;
		}

		public new void Dispose()
		{
			this.repository.Dispose();
			base.Dispose();
		}
	}
}
