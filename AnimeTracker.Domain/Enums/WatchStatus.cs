namespace AnimeTracker.Domain.Enums
{
    public enum WatchStatus
    {
        Watching = 1,       // İzliyorum
        Completed = 2,      // İzledim / Tamamlandı
        OnHold = 3,         // Beklemeye Aldım
        Dropped = 4,        // Yarıda Bıraktım
        PlanToWatch = 5     // İzleyeceğim
    }
}