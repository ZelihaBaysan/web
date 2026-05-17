namespace AnimeTracker.Application.Wrappers.Results
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
}
