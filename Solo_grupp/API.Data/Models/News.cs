namespace API.Data.Models
{
	#region Using
	using System;
	using System.Collections.Generic;
	#endregion
	public class News
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public virtual User User { get; set; }
		public virtual ICollection<Image> Images { get; set; }

		public News()
		{
			this.Id = Guid.NewGuid();
		}
	}
}