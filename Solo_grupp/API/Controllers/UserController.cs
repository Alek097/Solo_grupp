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
	#endregion
	public class UserController : ApiController
	{
		private ApplicationUserManager UserManager
		{
			get
			{
				return this.ControllerContext.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
		}
		public UserController()
		{

		}
		[AllowAnonymous]
		public async Task SignUp(RegistrationModel model)
		{
			User user = new User(model);
			IdentityResult result = await UserManager.CreateAsync(user, model.Password);
		}
	}
}
