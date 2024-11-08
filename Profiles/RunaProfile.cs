using AutoMapper;
using API_Bloodborne.Models;
using API_Bloodborne.Data.DTOs.Runas;

namespace API_Bloodborne.Profiles
{
    public class RunaProfile : Profile
    {
        public RunaProfile()
        {
            CreateMap<CreateRunaDto, Runa>();
            CreateMap<UpdateRunaDto, Runa>();
            CreateMap<Runa, UpdateRunaDto>();
            CreateMap<Runa, ReadRunaDto>();
        }
    }
}
