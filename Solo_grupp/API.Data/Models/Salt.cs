namespace API.Data.Models
{
	#region Using
	using System;
	#endregion
	public class Salt
	{
		public Guid Id { get; set; }
		public string Value { get; set; }
		public virtual User User { get; set; }

		public Salt()
		{
			this.Id = Guid.NewGuid();
			this.Value = string.Format("{0}-{1}-{2}",
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.NewGuid());
		}
	}
}