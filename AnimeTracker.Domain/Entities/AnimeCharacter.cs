using AnimeTracker.Domain.Common;

namespace AnimeTracker.Domain.Entities
{
    public class AnimeCharacter : BaseEntity
    {
        public Guid AnimeId { get; set; }
        public Anime Anime { get; set; } = null!;

        public Guid CharacterId { get; set; }
        public Character Character { get; set; } = null!;

        public Guid? VoiceActorId { get; set; }
        public VoiceActor? VoiceActor { get; set; }

        public string? Role { get; set; } // Main, Supporting vs.
    }
}
