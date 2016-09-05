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

			this.PasswordHash = User.HshPassword(user.Password, this.Salt);

			this.Permission = new List<Resolution>() {
				new Resolution() {ResolutionType = ResolutionType.AddComment }
			};
		}

		public static string HshPassword(string password, Salt salt)
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
