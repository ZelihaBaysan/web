using AnimeTracker.Domain.Common;

namespace AnimeTracker.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<AnimeGenre> AnimeGenres { get; set; } = new HashSet<AnimeGenre>();
    }
}