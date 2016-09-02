namespace API.Logging
{
	#region Using
	using System;
	#endregion
	[Serializable]
	public class ExceptionSerializel
	{
		public DateTime TimeStamp { get; set; }
		public string StackTrace { get; set; }
		public string Message { get; set; }
		public string Source { get; set; }
		public string Type { get; set; }
		public ExceptionSerializel InnerException { get; set; }
		public ExceptionSerializel()
		{
			this.TimeStamp = DateTime.Now;
		}
		public ExceptionSerializel(Exception ex) : this()
		{
			this.StackTrace = ex.StackTrace;
			this.Message = ex.Message;
			this.Source = ex.Source;
			this.Type = ex.GetType().ToString();
			this.InnerException = ex.InnerException == null ? null : new ExceptionSerializel(ex.InnerException);
		}
	}
}