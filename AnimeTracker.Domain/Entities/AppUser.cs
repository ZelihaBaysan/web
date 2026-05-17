using Microsoft.AspNetCore.Identity;

namespace AnimeTracker.Domain.Entities
{
    // Id tipini Guid olarak belirliyoruz ki diğer tablolarımızla (Review, WatchList) uyumlu olsun
    public class AppUser : IdentityUser<Guid>
    {
        public string? ProfileImageUrl { get; set; }

        // Navigation Properties (Bir kullanıcının neleri olabilir?)
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<WatchList> WatchLists { get; set; } = new HashSet<WatchList>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
        public ICollection<UserActivity> UserActivities { get; set; } = new HashSet<UserActivity>();
        public ICollection<Report> ReportsMade { get; set; } = new HashSet<Report>();
        public ICollection<Report> ReportsReceived { get; set; } = new HashSet<Report>();
    }
}