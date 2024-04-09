using AutoMapper;
using WebApi.Domain.DTOs;
using WebApi.Domain.Model;

namespace WebApi.Aplication.Mapa
{
    public class DomainToDTOsMapa : Profile
    {

        public DomainToDTOsMapa()
        {
            CreateMap<InfoApi, InfoApiDTO>()
                .ForMember(dest => dest.Name, m => m.MapFrom(orig => orig.name));
        }
    }
}
