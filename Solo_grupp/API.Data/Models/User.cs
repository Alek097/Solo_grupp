namespace API.Data.Models
{
	#region Using
	using System.Collections.Generic;
	using Microsoft.AspNet.Identity.EntityFramework;
	using API.Models;
	using System.Text;
	#endregion
	public class User : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Patronymic { get; set; }
		public string FullName { get; set; }
		public string Adress { get; set; }
		public virtual ICollection<Resolution> Permission { get; set; }
		public virtual ICollection<News> News { get; set; }

		public User()
		{

		}
		public User(RegistartionModel model)
		{
			this.firstUpper(model.FirstName);
			this.firstUpper(model.LastName);
			this.firstUpper(model.Patronymic);

			this.FirstName = model.FirstName;
			this.LastName = model.LastName;
			this.Patronymic = model.Patronymic;
			this.FullName = string.Format("{0} {1} {2}",
				model.LastName,
				model.FirstName,
				model.Patronymic);
			this.Adress = model.Adress;
			this.Email = model.Email;
			this.PhoneNumber = model.PhoneNumber;
		}

		private void firstUpper(string value)
		{
			StringBuilder builder = new StringBuilder(value);
			builder[0] = char.ToUpper(builder[0]);
			value = builder.ToString();
		}
	}
}
