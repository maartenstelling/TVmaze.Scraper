using AutoMapper;
using TVmaze.Scraper.Domain.Models;
using TVmaze.Scraper.Persistence.Entities;

namespace TVmaze.Scraper.Functions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Show, ShowEntity>().ForMember(dest => dest.ShowId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<Person, PersonEntity>().ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        }
    }
}
