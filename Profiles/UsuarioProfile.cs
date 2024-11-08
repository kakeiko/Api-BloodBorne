using AutoMapper;
using API_Bloodborne.Models;
using API_Bloodborne.Data.DTOs.Usuarios;

namespace API_Bloodborne.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<DeleteUsuarioDto, Usuario>();
        }
    }
}
