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
	#endregion
	public class UserController : ApiController
	{
		public UserController()
		{

		}
		[AllowAnonymous]
		public async Task SignUp(RegistrationModel model)
		{
			await Task.Run(() =>
			{

			});
		}
	}
}
