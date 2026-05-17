using AnimeTracker.Domain.Common;
using AnimeTracker.Domain.Enums;

namespace AnimeTracker.Domain.Entities
{
    public class WatchList : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!; // EKLENEN SATIR

        public Guid AnimeId { get; set; }
        public Anime Anime { get; set; } = null!;

        public WatchStatus Status { get; set; }
        public int WatchedEpisodes { get; set; } = 0;
        public int? PersonalScore { get; set; }
    }
}