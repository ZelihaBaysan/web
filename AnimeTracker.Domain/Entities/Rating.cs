using AnimeTracker.Domain.Common;

namespace AnimeTracker.Domain.Entities
{
    public class Rating : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public Guid AnimeId { get; set; }
        public Anime Anime { get; set; } = null!;

        public int Score { get; set; } // 1-10 arası
    }
}
