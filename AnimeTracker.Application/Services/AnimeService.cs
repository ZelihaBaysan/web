using AnimeTracker.Application.DTOs.Anime;
using AnimeTracker.Application.Interfaces;
using AnimeTracker.Application.Interfaces.Repositories;
using AnimeTracker.Application.Interfaces.Services;
using AnimeTracker.Domain.Entities;
using AutoMapper;

namespace AnimeTracker.Application.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IGenericRepository<Anime> _animeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AnimeService(IGenericRepository<Anime> animeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _animeRepository = animeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AnimeDto>> GetAllAnimesAsync()
        {
            var animes = await _animeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AnimeDto>>(animes);
        }

        public async Task<AnimeDto?> GetAnimeByIdAsync(Guid id)
        {
            var anime = await _animeRepository.GetByIdAsync(id);
            return _mapper.Map<AnimeDto>(anime);
        }

        public async Task<AnimeDto> CreateAnimeAsync(AnimeCreateDto createDto)
        {
            // 1. DTO'yu Veritabanı Entity'sine çevir
            var animeEntity = _mapper.Map<Anime>(createDto);

            // 2. Veritabanına Ekle ve Kaydet (Transaction)
            await _animeRepository.AddAsync(animeEntity);
            await _unitOfWork.CommitAsync();

            // 3. Eklenen veriyi tekrar DTO olarak geri dön
            return _mapper.Map<AnimeDto>(animeEntity);
        }
    }
}