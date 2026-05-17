using AnimeTracker.Application.DTOs.Anime;
using AnimeTracker.Application.Interfaces.Services;
using System.Net.Http.Json;

namespace AnimeTracker.Infrastructure.Services
{
    public partial class JikanService : IJikanService
    {
        private readonly HttpClient _httpClient;
        private readonly IAnimeService _animeService;

        public JikanService(HttpClient httpClient, IAnimeService animeService)
        {
            _httpClient = httpClient;
            _animeService = animeService;

            // Jikan API temel adresi
            _httpClient.BaseAddress = new Uri("https://api.jikan.moe/v4/");
        }

        public async Task SyncTopAnimesAsync()
        {
            // 1. Jikan API'den en popüler animeleri çek (Top Anime endpoint)
            var response = await _httpClient.GetFromJsonAsync<JikanAnimeResponse>("top/anime");

            if (response != null && response.Data != null)
            {
                foreach (var item in response.Data)
                {
                    // 2. Gelen veriyi bizim CreateDto formatına sokuyoruz
                    var createDto = new AnimeCreateDto
                    {
                        MalId = item.Mal_Id,
                        Title = item.Title,
                        Synopsis = item.Synopsis,
                        ImageUrl = item.Images?.Jpg?.Large_Image_Url,
                        Episodes = item.Episodes
                    };

                    // 3. Veritabanına kaydet (Daha önce yazdığımız servis metodu)
                    // Not: Gerçek projede burada "bu anime zaten var mı?" kontrolü yapılır.
                    await _animeService.CreateAnimeAsync(createDto);

                    // Jikan API rate limit (saniyede 3 istek) olduğu için küçük bir bekleme
                    await Task.Delay(500);
                }
            }
        }
    }
}