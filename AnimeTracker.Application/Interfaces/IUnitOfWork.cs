namespace AnimeTracker.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> CommitAsync();
    }
}