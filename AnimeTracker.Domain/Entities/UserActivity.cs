using AnimeTracker.Domain.Common;
using AnimeTracker.Domain.Enums;

namespace AnimeTracker.Domain.Entities
{
    public class UserActivity : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public ActivityType ActivityType { get; set; }

        public Guid? AnimeId { get; set; }
        public Anime? Anime { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
