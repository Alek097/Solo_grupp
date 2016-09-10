export class RepositoryResult<TResult>
{
    public Responce: TResult = null;
    public ResultType: number = null;
}
export class RepositoryResultValue<TValue, TResult> extends RepositoryResult<TResult>
{
    public Value: TValue = null;
}