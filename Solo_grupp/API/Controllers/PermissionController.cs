
namespace API.Controllers
{
	#region Using
	using Data.Models;
	using Filters;
	using System.Web.Http;
	#endregion
	public class PermissionController : ApiController
	{
		[Resolution(ResolutionType.AddNews)]
		public void IsAddNews() { }
	}
}
