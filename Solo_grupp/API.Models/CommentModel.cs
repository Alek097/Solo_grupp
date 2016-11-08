namespace API.Models
{
	#region Using
	using System;
	using System.Collections.Generic;
	#endregion
	public class CommentModel : IComparable<CommentModel>
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public DateTime CreateDate { get; set; }
		public virtual UserInformation Author { get; set; }
		public virtual List<CommentModel> Comments { get; set; }

		public int CompareTo(CommentModel other)
		{
			return this.CreateDate.CompareTo(other.CreateDate);
		}
	}
}
