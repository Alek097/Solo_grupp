﻿namespace API.Controllers
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
	#endregion
	public class UserController : ApiController
	{
		private readonly IUserRepository repository;
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
		public UserController(IUserRepository repository)
		{
			this.repository = repository;
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<MoveTo> SignUp(SignUp model)
		{
			if (!ModelState.IsValid)
			{
				string errorMessage = "";

				foreach (ModelState modelState in ModelState.Values)
				{
					foreach (ModelError error in modelState.Errors)
					{
						errorMessage = string.Format("{0}\n{1}");
					}
				}

				MoveTo responce = new MoveTo()
				{
					IsMoving = true,
					Location = string.Format("{0}/#/SignUp/{1}", Repository.DNS, errorMessage)
				};

				return responce;
			}

			NotActiveUser user = new NotActiveUser(model);
			RepositoryResult<MoveTo> result = await this.repository.RegistartionAsync(user);

			return result.Responce;
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<RepositoryResult<UserInformation, MoveTo>> SignIn(SignIn model)
		{
			RepositoryResult<UserInformation, MoveTo> result = new RepositoryResult<UserInformation, MoveTo>();

			RepositoryResult<User, MoveTo> repoResult = await this.repository.SignIn(model);

			result.Responce = repoResult.Responce;

			if (repoResult.Value == null)
			{
				return result;
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

				result.Value = new UserInformation()
				{
					Adress = repoResult.Value.Adress,
					Email = repoResult.Value.Email,
					FirstName = repoResult.Value.FirstName,
					FullName = repoResult.Value.FullName,
					Id = repoResult.Value.Id,
					LastName = repoResult.Value.LastName,
					Patronymic = repoResult.Value.Patronymic
				};

				return result;
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
	}
}
