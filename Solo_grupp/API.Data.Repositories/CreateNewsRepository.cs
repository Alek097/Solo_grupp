﻿namespace API.Data.Repositories
{
	#region Using
	using System.Threading.Tasks;
	using API.Models;
	using Interfaces;
	using Logging;
	using System.Web;
	using System.Text.RegularExpressions;
	using System.Collections.Generic;
	using Models;
	using System.IO;
	using System.Net;
	using System;
	using System.Linq;
	#endregion
	public class CreateNewsRepository : ICreateNewsRepository
	{
		private readonly IContext context;
		private readonly ILogger logger;
		public CreateNewsRepository(IContext context, ILogger logger)
		{
			this.context = context;
			this.logger = logger;
		}
		public async Task<RepositoryResult<News, ControllerResult>> CreateNews(CreateNews model)
		{
			RepositoryResult<News, ControllerResult> result = new RepositoryResult<News, ControllerResult>();

			MatchCollection matches = Regex.Matches(model.Content, "<img src=\"" + @".+?" + "\"" + @"[.\s]*/>");
			List<string> urls = new List<string>(model.Urls);

			News news = new News();

			List<Image> images = new List<Image>();

			foreach (Match match in matches)
			{
				string url = Regex.Match(match.ToString(), "src=\"" + @".+?" + "\"").ToString()
					.Replace("src=\"", string.Empty)
					.Replace("\"", string.Empty);

				if (urls.Contains(url))
				{
					urls.Remove(url);

					string serverPath = HttpContext.Current.Server.MapPath(url);

					string fileName = Path.GetFileName(serverPath);
					string newServerPath = HttpContext.Current.Server.MapPath(string.Format("~/Bundles/app/img/news/{0}", news.Id));

					if (!Directory.Exists(newServerPath))
					{
						Directory.CreateDirectory(newServerPath);
					}

					newServerPath = Path.Combine(newServerPath, fileName);

					try
					{
						File.Move(serverPath, newServerPath);

						this.logger.WriteInformation(string.Format("Файл успешно пермещён с {0} в {1}", serverPath, newServerPath));
					}
					catch (Exception ex)
					{

						this.logger.WriteError(ex, string.Format("Ошибка при перемещении файла по расположению {0} в {1}", serverPath, newServerPath));

						result.Responce = new ControllerResult()
						{
							IsSucces = false
						};

						result.ResultType = RepositoryResultType.Bad;

						return result;
					}

					images.Add(new Image()
					{
						URL = string.Format("~/Bundles/app/img/news/{0}/{1}", news.Id, fileName)
					});

				}
				else if (url.Contains("http://") || url.Contains("https://"))
				{
					string path = HttpContext.Current.Server.MapPath(string.Format("~/Bundles/app/img/news/{0}/", news.Id));

					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}
					string fileName = string.Format("{0}_{1}", Guid.NewGuid(), Guid.NewGuid());
					path = Path.Combine(path, fileName);

					using (WebClient WC = new WebClient())
					{
						try
						{
							WC.DownloadFile(url, path);
							this.logger.WriteInformation(string.Format("Загружен файл с внешнего ресурса по ссылке {0}.", url));
						}
						catch (Exception ex)
						{
							this.logger.WriteError(ex, string.Format("Ошибка при загрузке файла с внешнего ресурса по ссылке {0}.", url));

							result.Responce = new ControllerResult()
							{
								IsSucces = false
							};

							result.ResultType = RepositoryResultType.Bad;

							return result;
						}
					}

					images.Add(new Image()
					{
						URL = string.Format("~/Bundles/app/img/news/{0}/{1}", news.Id, fileName)
					});
				}
				else
				{
					result.Responce = new ControllerResult()
					{
						IsSucces = false
					};

					result.ResultType = RepositoryResultType.Bad;

					return result;
				}
			}

			news.Images = images;

			string userName = HttpContext.Current.User.Identity.Name;

			User user = context.GetAll<User>().FirstOrDefault(u => u.UserName == userName);

			news.User = user;

			context.Add(news);

			await this.context.SaveChangesAsync();

			foreach (string url in urls)
			{
				string path = HttpContext.Current.Server.MapPath(url);

				try
				{
					File.Delete(path);
					this.logger.WriteInformation(string.Format("Удалён файл по расположению", path));
				}
				catch (Exception ex)
				{
					this.logger.WriteError(ex, string.Format("Ошибка удаления файла по расположению", path));
				}
			}


			result.Responce = new ControllerResult()
			{
				IsSucces = true,
				Message = string.Format("/#/News/{0}", news.Id)
			};

			result.Value = news;

			result.ResultType = RepositoryResultType.Bad;

			return result;
		}

		public void Dispose()
		{
			context.Dispose();
		}
	}
}
