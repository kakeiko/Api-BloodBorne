using AutoMapper;
using API_Bloodborne.Models;
using API_Bloodborne.Data.DTOs.Personagens;

namespace API_Bloodborne.Profiles
{
    public class PersonagemProfile : Profile
    {
        public PersonagemProfile()
        {
            CreateMap<CreatePersonagemDto, Personagem>();
            CreateMap<UpdatePersonagemDto, Personagem>();
            CreateMap<Personagem, UpdatePersonagemDto>();
            CreateMap<Personagem, ReadPersonagemDto>();
        }
    }
}
