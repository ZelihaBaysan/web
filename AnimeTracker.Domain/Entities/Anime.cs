using AnimeTracker.Domain.Common;

namespace AnimeTracker.Domain.Entities
{
    public class Anime : BaseEntity
    {
        public int MalId { get; set; } // MyAnimeList üzerindeki orijinal ID
        public string Title { get; set; } = string.Empty;
        public string? TitleJapanese { get; set; }
        public string? Synopsis { get; set; } // Anime özeti/açıklaması
        public string? ImageUrl { get; set; }
        public string? BannerUrl { get; set; }
        public string? TrailerUrl { get; set; }

        public int? TotalEpisodes { get; set; }
        public decimal? Score { get; set; } // 9.15 gibi puanlar için
        public DateTime? AiredFrom { get; set; }
        public DateTime? AiredTo { get; set; }

        public string? Status { get; set; } // "Finished Airing", "Currently Airing" vb.
        
        public Guid? StudioId { get; set; }
        public Studio? Studio { get; set; }

        // İLİŞKİLER (Navigation Properties)
        public ICollection<AnimeGenre> AnimeGenres { get; set; } = new HashSet<AnimeGenre>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<WatchList> WatchLists { get; set; } = new HashSet<WatchList>();
        public ICollection<Episode> Episodes { get; set; } = new HashSet<Episode>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
        public ICollection<AnimeCharacter> AnimeCharacters { get; set; } = new HashSet<AnimeCharacter>();
    }
}