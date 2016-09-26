﻿namespace API.Controllers
{
	#region Using
	using Data.Models;
	using Data.Repositories;
	using Data.Repositories.Interfaces;
	using Filters;
	using Logging;
	using Models;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net.Http;
	using System.Threading.Tasks;
	using System.Web;
	using System.Web.Http;
	#endregion
	[Authorize]
	public class CreateNewsController : ApiController
	{
		private readonly ICreateNewsRepository repository;
		private readonly ILogger logger;
		public CreateNewsController(ICreateNewsRepository repository, ILogger logger)
		{
			this.repository = repository;
			this.logger = logger;
		}

		[HttpPost]
		[Resolution(ResolutionType.AddNews)]
		public async Task<UploadResult> UploadImage()
		{
			if (!Request.Content.IsMimeMultipartContent())
			{
				return new UploadResult()
				{
					IsUploading = false
				};
			}

			List<string> urls = new List<string>();
			#region images extensions
			List<string> extensions = new List<string>(new string[]
			{
				".JPG",
				".JFIF",
				".JPE",
				".JPEG",
				".GIF",
				".PNG",
				".SVG",
				".SVGZ",
				".TIF",
				".TIFF",
				".ICO",
				".WBMP",
				".WEBP"
			});
			#endregion

			var provider = new MultipartMemoryStreamProvider();

			string root = HttpContext.Current.Server.MapPath("~/Bundles/app/img/tmp");


			await Request.Content.ReadAsMultipartAsync(provider);


			foreach (var file in provider.Contents)
			{
				string fileExtension = Path.GetExtension(file.Headers.ContentDisposition.FileName.Trim('\"'));

				if (extensions.Contains(fileExtension.ToUpper()))
				{
					string fileName = string.Format("{0}_{1}{2}",
						Guid.NewGuid(),
						Guid.NewGuid(),
						fileExtension);

					urls.Add(string.Format("/Bundles/app/img/tmp/{0}", fileName));

					byte[] fileArray = await file.ReadAsByteArrayAsync();

					using (FileStream fs = new FileStream(Path.Combine(root, fileName), FileMode.Create))
					{
						await fs.WriteAsync(fileArray, 0, fileArray.Length);
					}
				}
			}

			return new UploadResult()
			{
				IsUploading = true,
				Urls = urls
			};
		}
		[HttpPost]
		[Resolution(ResolutionType.AddNews)]
		public async Task<MoveTo> Create(CreateNews model)
		{
			this.logger.WriteInformation("Запрос на создание новости");

			RepositoryResult<MoveTo> result = await this.repository.CreateNews(model);

			if(result.ResultType == RepositoryResultType.OK)
			{
				this.logger.WriteInformation("Новость успешно создана");
			}
			else
			{
				this.logger.WriteInformation("Ошибка при создании новости");
			}
			return result.Responce;
		}
	}
}
