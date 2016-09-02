namespace API.Data.Repositories
{
	#region Using
	using System.Net.Http;
	#endregion
	public class RepositoryResult<TValue>
	{
		public TValue Value { get; set; }
		public HttpResponseMessage Responce { get; set; }
		public RepositoryResultType ResultType { get; set; }
	}
}
