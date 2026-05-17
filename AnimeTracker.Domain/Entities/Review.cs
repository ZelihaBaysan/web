using AnimeTracker.Domain.Common;

namespace AnimeTracker.Domain.Entities
{
    public class Review : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!; // EKLENEN SATIR

        public Guid AnimeId { get; set; }
        public Anime Anime { get; set; } = null!;

        public string Content { get; set; } = string.Empty;
        public int Score { get; set; }
        public bool IsSpoiler { get; set; } = false;
    }
}