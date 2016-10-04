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
		public new string PasswordHash { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public Guid ReplaceCode { get; set; }
		public DateTime Birthday { get; set; }
		public virtual Salt Salt { get; set; }
		public virtual ICollection<Resolution> Permission { get; set; }
		public virtual ICollection<News> News { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }

		public User()
		{

		}

		public User(NotActiveUser user)
		{
			this.UserName = Guid.NewGuid().ToString().Replace("-", string.Empty);
			this.FirstName = user.FirstName;
			this.LastName = user.LastName;
			this.Patronymic = user.Patronymic;
			this.Country = user.Country;
			this.City = user.City;
			this.Email = user.Email;
			this.PhoneNumber = user.PhoneNumber;
			this.FullName = string.Format("{0} {1} {2}", this.LastName, this.FirstName, this.Patronymic);
			this.Birthday = user.Birthday;

			this.Salt = new Salt();

			this.PasswordHash = User.HashPassword(user.Password, this.Salt);

			this.Permission = new List<Resolution>() {
				new Resolution() {ResolutionType = ResolutionType.AddComment }
			};
		}

		public static string HashPassword(string password, Salt salt)
		{

			byte[] bytesPaassword = Encoding.UTF8.GetBytes(password);
			SHA256Managed sha256 = new SHA256Managed();
			byte[] bytesHashPass = sha256.ComputeHash(bytesPaassword);
			string hashPassword = Encoding.UTF8.GetString(bytesHashPass);

			hashPassword += salt.Value;

			bytesPaassword = Encoding.UTF8.GetBytes(hashPassword);
			bytesHashPass = sha256.ComputeHash(bytesPaassword);

			return Encoding.UTF8.GetString(bytesHashPass);
		}
	}
}
