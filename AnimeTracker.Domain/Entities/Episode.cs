using AnimeTracker.Domain.Common;

namespace AnimeTracker.Domain.Entities
{
    public class Episode : BaseEntity
    {
        public int MalId { get; set; }
        public Guid AnimeId { get; set; }
        public Anime Anime { get; set; } = null!;

        public string? Title { get; set; }
        public string? TitleJapanese { get; set; }
        public int EpisodeNumber { get; set; }
        public DateTime? Aired { get; set; }
        public decimal? Score { get; set; }
    }
}
