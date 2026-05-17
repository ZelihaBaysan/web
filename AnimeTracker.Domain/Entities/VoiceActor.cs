using AnimeTracker.Domain.Common;

namespace AnimeTracker.Domain.Entities
{
    public class VoiceActor : BaseEntity
    {
        public int MalId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Language { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<AnimeCharacter> AnimeCharacters { get; set; } = new HashSet<AnimeCharacter>();
    }
}
