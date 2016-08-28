namespace API.App_Start
{
	#region Using
	using Data;
	using Owin;
	using Microsoft.Owin.Security.Cookies;
	using Microsoft.AspNet.Identity;
	using Microsoft.Owin;
	#endregion
	public static class IdentityConfig
	{
		public static void Config(IAppBuilder app)
		{
			app.CreatePerOwinContext<ApplicationContext>(ApplicationContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
			});
		}
	}
}