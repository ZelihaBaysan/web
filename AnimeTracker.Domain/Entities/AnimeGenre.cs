using AnimeTracker.Domain.Common;

namespace AnimeTracker.Domain.Entities
{
    public class AnimeGenre : BaseEntity
    {
        public Guid AnimeId { get; set; }
        public Anime Anime { get; set; } = null!;

        public Guid GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
    }
}