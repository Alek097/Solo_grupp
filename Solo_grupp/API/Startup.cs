[assembly: Microsoft.Owin.OwinStartup(typeof(API.Startup))]

namespace API
{
	#region Using
	using Owin;
	using System.Web.Http;
	using App_Start;
	#endregion
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var config = new HttpConfiguration();

			ContainerConfig.Config(config);
			IdentityConfig.Config(app);
			WebApiConfig.RegisterRoutes(config);

			config.MapHttpAttributeRoutes();
			app.UseWebApi(config);

		}
	}
}
