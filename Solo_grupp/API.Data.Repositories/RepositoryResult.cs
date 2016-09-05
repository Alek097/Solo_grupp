namespace API.Data.Repositories
{
	#region Using
	using System.Net.Http;
	#endregion
	public class RepositoryResult<TValue,TResult> : RepositoryResult<TResult>
	{
		public TValue Value { get; set; }
	}
	public class RepositoryResult<TResult>
	{
		public TResult Responce { get; set; }
		public RepositoryResultType ResultType { get; set; }
	}
}
