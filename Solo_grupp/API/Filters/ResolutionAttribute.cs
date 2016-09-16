namespace API.Filters
{
	#region Using
	using Data;
	using Data.Models;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Web;
	using System.Web.Http;
	using System.Web.Http.Controllers;
	#endregion
	public class ResolutionAttribute : AuthorizeAttribute
	{
		private List<ResolutionType> permission;
		private ApplicationContext context = DependencyContainer.GetType<ApplicationContext>();

		public ResolutionAttribute(params ResolutionType[] permission)
		{
			this.permission = new List<ResolutionType>(permission);
		}
		public override void OnAuthorization(HttpActionContext actionContext)
		{
			base.OnAuthorization(actionContext);

			if (actionContext.Response == null)
			{
				string userName = HttpContext.Current.User.Identity.Name;

				User user = context.GetAll<User>().FirstOrDefault(u => u.UserName == userName);

				if (user == null)
				{
					actionContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound);

					this.context.Dispose();

					return;
				}
				else
				{
					bool succes = true;

					foreach (Resolution resolution in user.Permission)
					{
						succes = succes && this.permission.Contains(resolution.ResolutionType);
					}

					if (!succes)
					{
						actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
					}

				}

			}

			this.context.Dispose();

		}
	}
}