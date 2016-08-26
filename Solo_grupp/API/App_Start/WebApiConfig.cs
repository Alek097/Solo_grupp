using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace API
{
    public static class WebApiConfig
    {
		public static void RegisterRoutes(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				"API Default",
				"api/{controller}/{action}",
				new { id = RouteParameter.Optional }
				);
		}
	}
}
