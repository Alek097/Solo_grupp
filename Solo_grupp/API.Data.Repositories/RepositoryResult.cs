namespace API.Data.Repositories
{
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
