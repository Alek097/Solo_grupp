using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(API.Startup))]

namespace API
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var config = new HttpConfiguration();
			WebApiConfig.RegisterRoutes(config);
			config.MapHttpAttributeRoutes();
			app.UseWebApi(config);

		}
	}
}
