namespace API.Models
{
	#region Using
	using System.Collections.Generic;
	#endregion
	public class UploadResult
	{
		public bool IsUploading { get; set; }
		public List<string> Urls { get; set; }
	}
}
