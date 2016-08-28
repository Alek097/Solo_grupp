namespace API.App_Start
{
	#region Using
	using Data;
	using Owin;
	using Microsoft.Owin.Security.Cookies;
	using Microsoft.AspNet.Identity;
	using System.Web.Http;
	using Microsoft.Practices.Unity;
	#endregion
	public static class IdentityConfig
	{
		public static void Config(IAppBuilder app, HttpConfiguration config)
		{
			UnityResolver container = config.DependencyResolver as UnityResolver;
			app.CreatePerOwinContext<ApplicationContext>(()=> container.Container.Resolve<ApplicationContext>());
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
			});
		}
	}
}