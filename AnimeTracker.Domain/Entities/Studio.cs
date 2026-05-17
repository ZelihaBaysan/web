using AnimeTracker.Domain.Common;

namespace AnimeTracker.Domain.Entities
{
    public class Studio : BaseEntity
    {
        public int MalId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? Established { get; set; }
        
        public ICollection<Anime> Animes { get; set; } = new HashSet<Anime>();
    }
}
