using AutoMapper;
using WebApi.Domain.DTOs;
using WebApi.Domain.Model;

namespace WebApi.Aplication.Map
{
    public class DomainToDTOsMap : Profile
    {

        public DomainToDTOsMap()
        {
            CreateMap<InfoApi, InfoApiDTO>()
                .ForMember(dest => dest.Name, m => m.MapFrom(orig => orig.name));
        }
    }
}
