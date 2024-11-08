using AutoMapper;
using API_Bloodborne.Models;
using API_Bloodborne.Data.DTOs.Locais;

namespace API_Bloodborne.Profiles
{
    public class LocalProfile : Profile
    {
        public LocalProfile()
        {
            CreateMap<CreateLocalDto, Local>();
            CreateMap<UpdateLocalDto, Local>();
            CreateMap<Local, UpdateLocalDto>();
            CreateMap<Local, ReadLocalDto>();
        }
    }
}
