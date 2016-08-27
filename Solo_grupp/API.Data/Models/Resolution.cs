namespace API.Data.Models
{
	#region Using
	using System;
	using System.ComponentModel.DataAnnotations.Schema;
	#endregion
	public class Resolution
	{
		public Guid Id { get; set; }
		[NotMapped]
		public ResolutionType ResolutionType {
			get
			{
				return this.resolutionType;
			}
			set
			{
				this.resolutionType = value;
				this.resolutionName = value.ToString();
			}
		}
		public string ResolutionName
		{
			get
			{
				return this.resolutionName;
			}
			set
			{
				this.resolutionName = value;
				this.resolutionType = (ResolutionType)Enum.Parse(typeof(ResolutionType), value);
			}
		}
		public virtual User User { get; set; }

		private ResolutionType resolutionType;
		private string resolutionName;

		public Resolution()
		{
			this.Id = Guid.NewGuid();
		}
	}
}