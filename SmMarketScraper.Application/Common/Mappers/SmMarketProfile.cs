using AutoMapper;
using SmMarketScraper.Application.Shared.Models;
using SmMarketScraper.Domain.Entities;

namespace SmMarketScraper.Application.Shared.Mappers;

public class SmMarketProfile : Profile
{
    public SmMarketProfile()
    {
        CreateMap<SmMarketItem, Product>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.DescriptionHtml))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image));
    }
}