namespace API.Models
{
	#region Using
	using System;
	using System.ComponentModel.DataAnnotations;
	#endregion
	public class Replace
	{
		[RegularExpression("[a-z0-9]{8}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{12}", ErrorMessage = "Неправильный код.")]
		public Guid ReplaceCode { get; set; }
		[Required(ErrorMessage = "Отсутствует пароль")]
		[MinLength(5, ErrorMessage = "Пароль слишком короткий")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Повторите пароль")]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		public string RepeatedPassword { get; set; }
	}
}
