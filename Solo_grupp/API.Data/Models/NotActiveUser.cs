namespace API.Data.Models
{
	#region Using
	using API.Models;
	using System;
	using System.Text;
	#endregion
	public class NotActiveUser
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Patronymic { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public DateTime Birthday { get; set; }

		public NotActiveUser()
		{

		}
		public NotActiveUser(SignUp model)
		{
			this.Id = Guid.NewGuid();
			this.firstUpper(model.FirstName);
			this.firstUpper(model.LastName);
			this.firstUpper(model.Patronymic);

			this.FirstName = model.FirstName;
			this.LastName = model.LastName;
			this.Patronymic = model.Patronymic;
			this.Email = model.Email;
			this.PhoneNumber = model.PhoneNumber;
			this.Password = model.Password;
			this.Country = model.Country;
			this.City = model.City;
			this.Birthday = model.Date;
		}
		private void firstUpper(string value)
		{
			StringBuilder builder = new StringBuilder(value);
			builder[0] = char.ToUpper(builder[0]);
			value = builder.ToString();
		}
	}
}

