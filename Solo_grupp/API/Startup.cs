[assembly: Microsoft.Owin.OwinStartup(typeof(API.Startup))]

namespace API
{
	#region Using
	using Owin;
	using System.Web.Http;
	using App_Start;
	using System;
	using Logging;
	#endregion
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			try
			{
				var config = new HttpConfiguration();

				ContainerConfig.Config(config);
				IdentityConfig.Config(app, config);
				WebApiConfig.RegisterRoutes(config);

				config.MapHttpAttributeRoutes();
				app.UseWebApi(config);
			}
			catch(Exception ex)
			{
				Logger logger = new Logger();
				logger.WriteFatal(ex, "Произошла фатальная ошибка при запуске проекта, дальнейшая работа невозможна");
			}
		}
	}
}
