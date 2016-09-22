namespace API.Filters
{
	#region Using
	using Data;
	using Data.Models;
	using Logging;
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

				using (ApplicationContext context = new ApplicationContext(new Logger()))
				{

					User user = context.GetAll<User>().FirstOrDefault(u => u.UserName == userName);

					if (user == null)
					{
						actionContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound);

						return;
					}
					else
					{
						bool succes = true;

						foreach (ResolutionType resolutionType in this.permission)
						{
							Resolution resolution = user.Permission.FirstOrDefault(p=>p.ResolutionType == resolutionType);

							succes = succes && (resolution != null);
						}

						if (!succes)
						{
							actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
						}

					}
				}
			}

		}
	}
}