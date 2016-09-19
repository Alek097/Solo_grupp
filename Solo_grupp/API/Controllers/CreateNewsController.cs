namespace API.Controllers
{
	#region Using
	using Data.Models;
	using Filters;
	using Models;
	using System;
	using System.IO;
	using System.Net.Http;
	using System.Threading.Tasks;
	using System.Web.Http;
	#endregion
	[Authorize]
	public class CreateNewsController : ApiController
    {
		//public async Task<UploadResult> UploadImage()
		//{
		//	if (!Request.Content.IsMimeMultipartContent())
		//	{
		//		return new UploadResult()
		//		{
		//			IsUploading = false
		//		};
		//	}
		//	var provider = new MultipartMemoryStreamProvider();
		//	// путь к папке на сервере
		//	string root = System.Web.HttpContext.Current.Server.MapPath("~/../temp_img/");
		//	await Request.Content.ReadAsMultipartAsync(provider);

		//	foreach (var file in provider.Contents)
		//	{
		//		string fileName = Guid.NewGuid().ToString();
		//		byte[] fileArray = await file.ReadAsByteArrayAsync();

		//		using (FileStream fs = new FileStream(root + fileName, FileMode.Create))
		//		{
		//			await fs.WriteAsync(fileArray, 0, fileArray.Length);
		//		}
		//	}
		//}
    }
}
