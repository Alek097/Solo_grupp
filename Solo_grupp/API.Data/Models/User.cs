namespace API.Data.Models
{
	#region Using
	using System;
	using System.Collections.Generic;
	#endregion
	public class User
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Patronymic { get; set; }
		public string FullName { get; set; }
		public virtual ICollection<Resolution> Permission { get; set; }
		public virtual ICollection<News> News { get; set; }

		public User()
		{
			this.Id = Guid.NewGuid();
		}
	}
}
