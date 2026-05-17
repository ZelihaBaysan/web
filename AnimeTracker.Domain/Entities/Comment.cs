using AnimeTracker.Domain.Common;

namespace AnimeTracker.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public Guid AnimeId { get; set; }
        public Anime Anime { get; set; } = null!;

        public string Content { get; set; } = string.Empty;
        public bool IsSpoiler { get; set; } = false;

        public int Likes { get; set; } = 0;
        public int Dislikes { get; set; } = 0;

        // Nested Comment System (Self-referencing)
        public Guid? ParentCommentId { get; set; }
        public Comment? ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; } = new HashSet<Comment>();
    }
}
