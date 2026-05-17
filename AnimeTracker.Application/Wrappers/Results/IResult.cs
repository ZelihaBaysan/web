namespace AnimeTracker.Application.Wrappers.Results
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
