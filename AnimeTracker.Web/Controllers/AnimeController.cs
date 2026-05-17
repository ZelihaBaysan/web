using AnimeTracker.Application.DTOs.Anime;
using AnimeTracker.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimeTracker.Web.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IAnimeService _animeService;
        private readonly IJikanService _jikanService; // Jikan servisi eklendi

        public AnimeController(IAnimeService animeService, IJikanService jikanService)
        {
            _animeService = animeService;
            _jikanService = jikanService;
        }

        // GET: /Anime/Index
        public async Task<IActionResult> Index()
        {
            var animes = await _animeService.GetAllAnimesAsync();
            return View(animes);
        }

        // GET: /Anime/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Anime/Create
        [HttpPost]
        public async Task<IActionResult> Create(AnimeCreateDto createDto)
        {
            if (ModelState.IsValid)
            {
                await _animeService.CreateAnimeAsync(createDto);
                return RedirectToAction(nameof(Index));
            }
            return View(createDto);
        }

        // YENİ: /Anime/Sync (Dış API'den verileri çeker)
        // Bu adrese gittiğinde sistem otomatik olarak Jikan üzerinden verileri doldurur.
        public async Task<IActionResult> Sync()
        {
            try
            {
                await _jikanService.SyncTopAnimesAsync();
                TempData["SuccessMessage"] = "Animeler başarıyla senkronize edildi!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Senkronizasyon sırasında bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}