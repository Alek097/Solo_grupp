namespace API.Models
{
	public class ControllerResult
	{
		public string Message { get; set; }
		public bool IsSucces { get; set; }
	}

	public class ControllerResult<TValue> : ControllerResult
	{
		public TValue Value { get; set; }
	}
}
