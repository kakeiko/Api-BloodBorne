using AutoMapper;
using API_Bloodborne.Models;
using API_Bloodborne.Data.DTOs.Roupas;

namespace API_Bloodborne.Profiles
{
    public class RoupaProfile : Profile
    {
        public RoupaProfile()
        {
            CreateMap<CreateRoupaDto, Roupa>();
            CreateMap<UpdateRoupaDto, Roupa>();
            CreateMap<Roupa, UpdateRoupaDto>();
            CreateMap<Roupa, ReadRoupaDto>();
        }
    }
}
