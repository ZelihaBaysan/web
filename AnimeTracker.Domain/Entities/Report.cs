using AnimeTracker.Domain.Common;

namespace AnimeTracker.Domain.Entities
{
    public class Report : BaseEntity
    {
        public Guid ReporterId { get; set; }
        public AppUser Reporter { get; set; } = null!;

        public Guid? ReportedUserId { get; set; }
        public AppUser? ReportedUser { get; set; }

        public Guid? CommentId { get; set; }
        public Comment? Comment { get; set; }

        public string Reason { get; set; } = string.Empty;
        public bool IsResolved { get; set; } = false;
    }
}
