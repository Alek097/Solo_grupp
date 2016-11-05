namespace API.Models
{
	#region Using
	using System;
	using System.Collections.Generic;
	#endregion
	public class Comment
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public DateTime CreateDate { get; set; }
		public virtual UserInformation Author { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }
	}
}
