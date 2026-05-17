namespace AnimeTracker.Application.Interfaces.Services
{
    public interface IJikanService
    {
        Task SyncTopAnimesAsync(); // En popüler animeleri senkronize et
    }
}