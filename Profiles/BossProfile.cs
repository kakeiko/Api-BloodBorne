using AutoMapper;
using API_Bloodborne.Models;
using API_Bloodborne.Data.DTOs.Bosses;

namespace API_Bloodborne.Profiles
{
    public class BossProfile : Profile
    {
        public BossProfile()
        {
            CreateMap<CreateBossDto, Boss>();
            CreateMap<UpdateBossDto, Boss>();
            CreateMap<Boss, UpdateBossDto>();
            CreateMap<Boss, ReadBossDto>();
        }
    }
}
