namespace API.Models
{
	#region Using
	using System;
	using System.ComponentModel.DataAnnotations;
	#endregion
	public class SignUp
	{
		[Required(ErrorMessage = "Отсутствует имя")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Отсутствует фамилия")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Отсутствует отчество")]
		public string Patronymic { get; set; }
		[Required(ErrorMessage = "Отсутствует пароль")]
		[MinLength(5, ErrorMessage = "Пароль слишком короткий")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Повторите пароль")]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		public string RepeatedPassword { get; set; }
		[Required(ErrorMessage = "Отсутствует страна")]
		public string Country { get; set; }
		[Required(ErrorMessage = "Отсутствует страна")]
		public string City { get; set; }
		[Required(ErrorMessage = "Отсутствует E-mail")]
		[RegularExpression(@"^[-._a-z0-9]+@(?:[a-z0-9][-a-z0-9]+\.)+[a-z]{2,6}$")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Отсутствует номер телефона")]
		public string PhoneNumber { get; set; }
		[Required(ErrorMessage = "Отсутствует дата рождения")]
		public DateTime Date { get; set; }
	}
}