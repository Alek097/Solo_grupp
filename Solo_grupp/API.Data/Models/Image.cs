namespace API.Data.Models
{
	#region Using
	using System;
	#endregion
	public class Image
	{
		public Guid Id { get; set; }
		public string URL { get; set; }
		public virtual News News { get; set; }

		public Image()
		{
			this.Id = Guid.NewGuid();
		}
	}
}