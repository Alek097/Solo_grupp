export class RepositoryResult<TResult>
{
    public Responce: TResult;
    public ResultType: number;
}
export class RepositoryResultValue<TValue, TResult> extends RepositoryResult<TResult>
{
    public Value: TValue;
}