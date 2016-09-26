
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
		[HttpGet]
		public void IsAddNews() { }

		[Resolution(ResolutionType.EditRankOfUser)]
		[HttpGet]
		public void IsEditRankOfUser() { }

		[Resolution(ResolutionType.AddProduct)]
		[HttpGet]
		public void IsAddProduct() { }

		[Resolution(ResolutionType.EditProduct)]
		[HttpGet]
		public void IsEditProduct() { }

		[Resolution(ResolutionType.DeleteProduct)]
		[HttpGet]
		public void IsDeleteProduct() { }

		[Resolution(ResolutionType.DeleteNews)]
		[HttpGet]
		public void IsDeleteNews() { }

		[Resolution(ResolutionType.EditNews)]
		[HttpGet]
		public void IsEditNews() { }

		[Resolution(ResolutionType.AddComment)]
		[HttpGet]
		public void IsAddComment() { }

		[Resolution(ResolutionType.DeleteUserComment)]
		[HttpGet]
		public void IsDeleteUserComment() { }

		[Resolution(ResolutionType.EditUserComment)]
		[HttpGet]
		public void IsEditUserComment() { }
	}
}
