using AnimeTracker.Application.DTOs.Anime;
using AnimeTracker.Domain.Entities;
using AutoMapper;

namespace AnimeTracker.Application.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // ReverseMap, dönüşümün çift yönlü (Anime <-> AnimeDto) olmasını sağlar
            CreateMap<Anime, AnimeDto>().ReverseMap();
            CreateMap<Anime, AnimeCreateDto>().ReverseMap();
        }
    }
}