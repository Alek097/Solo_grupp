using System;


namespace API.Models
{
	public class NewsModel
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime CreateDate { get; set; }
		public UserInformation Author { get; set; }
	}
}
