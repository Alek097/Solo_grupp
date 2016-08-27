namespace API.Data.Models
{
	#region Using
	using System;
	using System.Collections.Generic;
	using Microsoft.AspNet.Identity.EntityFramework;
	#endregion
	public class User : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Patronymic { get; set; }
		public string FullName { get; set; }
		public virtual ICollection<Resolution> Permission { get; set; }
		public virtual ICollection<News> News { get; set; }
	}
}
