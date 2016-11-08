namespace API.Data.Models
{
	#region Using
	using System;
	using System.Collections.Generic;
	using API.Models;
	#endregion
	public class Comment
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public DateTime CreateDate { get; set; }
		public virtual User Author { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }

		public static explicit operator CommentModel(Comment comment)
		{
			CommentModel model = new CommentModel();
			model.Id = comment.Id;
			model.Text = comment.Text;
			model.CreateDate = comment.CreateDate;
			model.Comments = new List<CommentModel>();
			model.Author = (UserInformation)comment.Author;

			foreach (Comment item in comment.Comments)
			{
				model.Comments.Add((CommentModel)item);
			}

			model.Comments.Sort();
			model.Comments.Reverse();

			return model;
		}
	}
}
