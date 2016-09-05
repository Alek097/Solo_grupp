namespace API.Data.Models
{
	#region Using
	using System.Collections.Generic;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System.Security.Cryptography;
	using System.Text;
	using System;
	#endregion
	public class User : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Patronymic { get; set; }
		public string FullName { get; set; }
		public string Adress { get; set; }
		public virtual Salt Salt { get; set; }
		public virtual ICollection<Resolution> Permission { get; set; }
		public virtual ICollection<News> News { get; set; }

		public User()
		{

		}

		public User(NotActiveUser user)
		{
			this.UserName = Guid.NewGuid().ToString().Replace("-", string.Empty);
			this.FirstName = user.FirstName;
			this.LastName = user.LastName;
			this.Patronymic = user.Patronymic;
			this.Adress = user.Adress;
			this.Email = user.Email;
			this.PhoneNumber = user.PhoneNumber;

			this.Salt = new Salt();

			byte[] bytesPaassword = Encoding.UTF8.GetBytes(user.Password);
			SHA256Managed sha256 = new SHA256Managed();
			byte[] hashPass = sha256.ComputeHash(bytesPaassword);
			this.PasswordHash = Encoding.UTF8.GetString(hashPass);

			this.Permission = new List<Resolution>() {
				new Resolution() {ResolutionType = ResolutionType.AddComment }
			};
		}
	}
}
