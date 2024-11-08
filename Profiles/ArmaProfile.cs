using AutoMapper;
using API_Bloodborne.Models;
using API_Bloodborne.Data.DTOs.Armas;

namespace API_Bloodborne.Profiles
{
    public class ArmaProfile : Profile
    {
        public ArmaProfile()
        {
            CreateMap<CreateArmaDto, Arma>();
            CreateMap<UpdateArmaDto, Arma>();
            CreateMap<Arma, UpdateArmaDto>();
            CreateMap<Arma, ReadArmaDto>();
        }
    }
}
