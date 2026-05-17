namespace AnimeTracker.Application.DTOs.Anime
{
    public class AnimeDto
    {
        public Guid Id { get; set; }
        public int MalId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Synopsis { get; set; }
        public string? ImageUrl { get; set; }
        public int? Episodes { get; set; }
        public decimal? Score { get; set; }
        public string? Status { get; set; }
    }
}