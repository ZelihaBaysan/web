namespace AnimeTracker.Application.DTOs.Anime
{
    // Jikan API'den gelen verinin yapısı (JSON'dan C#'a eşleme)
    public class JikanAnimeResponse
    {
        public List<JikanAnimeData> Data { get; set; } = new();
    }

    public class JikanAnimeData
    {
        public int Mal_Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Synopsis { get; set; }
        public JikanImages? Images { get; set; }
        public int? Episodes { get; set; }
        public double? Score { get; set; }
        public string? Status { get; set; }
    }

    public class JikanImages
    {
        public JikanJpg? Jpg { get; set; }
    }

    public class JikanJpg
    {
        public string? Large_Image_Url { get; set; }
    }
}