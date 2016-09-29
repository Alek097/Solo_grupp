namespace API.Models
{
	#region Using
	using System.ComponentModel.DataAnnotations;
	#endregion
	public class CreateNews
	{
		[Required(ErrorMessage = "Отсутствует заголовок")]
		public string Title { get; set; }
		[Required(ErrorMessage = "Отсутствует контент")]
		public string Content { get; set; }
		public string[] Urls { get; set; }
	}
}
