namespace API.Controllers
{
	#region Using
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
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
		public UserController(IUserRepository repository)
		{
			this.repository = repository;
		}
		[AllowAnonymous]
		public async Task SignUp(RegistrationModel model)
		{
			NotActiveUser user = new NotActiveUser(model);
			await this.repository.RegistartionAsync(user);
		}
		[AllowAnonymous]
		public async Task<HttpResponseMessage> Activation(Guid id)
		{
			RepositoryResult<User> result = await this.repository.Activation(id);

			if (result.ResultType == RepositoryResultType.OK)
			{
				IdentityResult identityResult = await this.UserManager.CreateAsync(result.Value, result.Value.PasswordHash);
			}

			return result.Responce;
		}
	}
}
