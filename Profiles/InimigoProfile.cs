using AutoMapper;
using API_Bloodborne.Models;
using API_Bloodborne.Data.DTOs.Inimigos;

namespace API_Bloodborne.Profiles
{
    public class InimigoProfile : Profile
    {
        public InimigoProfile()
        {
            CreateMap<CreateInimigoDto, Inimigo>();
            CreateMap<UpdateInimigoDto, Inimigo>();
            CreateMap<Inimigo, UpdateInimigoDto>();
            CreateMap<Inimigo, ReadInimigoDto>();
        }
    }
}
