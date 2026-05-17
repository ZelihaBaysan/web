using AnimeTracker.Domain.Entities;
using AnimeTracker.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AnimeTracker.Persistence.Contexts
{
    // DİKKAT: Artık DbContext değil, IdentityDbContext'ten miras alıyoruz!
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Anime> Animes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<AnimeGenre> AnimeGenres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<VoiceActor> VoiceActors { get; set; }
        public DbSet<AnimeCharacter> AnimeCharacters { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Identity tablolarının oluşması için bu satır ŞARTTIR:
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AnimeGenre>()
                .HasKey(ag => new { ag.AnimeId, ag.GenreId });

            modelBuilder.Entity<AnimeGenre>()
                .HasOne(ag => ag.Anime)
                .WithMany(a => a.AnimeGenres)
                .HasForeignKey(ag => ag.AnimeId);

            modelBuilder.Entity<AnimeGenre>()
                .HasOne(ag => ag.Genre)
                .WithMany(g => g.AnimeGenres)
                .HasForeignKey(ag => ag.GenreId);

            modelBuilder.Entity<WatchList>()
                .HasIndex(w => new { w.UserId, w.AnimeId })
                .IsUnique();

            // AnimeCharacter Junction
            modelBuilder.Entity<AnimeCharacter>()
                .HasKey(ac => new { ac.AnimeId, ac.CharacterId });

            modelBuilder.Entity<AnimeCharacter>()
                .HasOne(ac => ac.Anime)
                .WithMany(a => a.AnimeCharacters)
                .HasForeignKey(ac => ac.AnimeId);

            modelBuilder.Entity<AnimeCharacter>()
                .HasOne(ac => ac.Character)
                .WithMany(c => c.AnimeCharacters)
                .HasForeignKey(ac => ac.CharacterId);
                
            modelBuilder.Entity<AnimeCharacter>()
                .HasOne(ac => ac.VoiceActor)
                .WithMany(v => v.AnimeCharacters)
                .HasForeignKey(ac => ac.VoiceActorId)
                .IsRequired(false);

            // Comments (Self-referencing & User/Anime)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Anime)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AnimeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Rating
            modelBuilder.Entity<Rating>()
                .HasIndex(r => new { r.UserId, r.AnimeId })
                .IsUnique();
                
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Report
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Reporter)
                .WithMany(u => u.ReportsMade)
                .HasForeignKey(r => r.ReporterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.ReportedUser)
                .WithMany(u => u.ReportsReceived)
                .HasForeignKey(r => r.ReportedUserId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // UserActivity
            modelBuilder.Entity<UserActivity>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserActivities)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Studio
            modelBuilder.Entity<Anime>()
                .HasOne(a => a.Studio)
                .WithMany(s => s.Animes)
                .HasForeignKey(a => a.StudioId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}