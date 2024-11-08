using API_Bloodborne.Data;
using API_Bloodborne.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using API_Bloodborne.Data.DTOs.Usuarios;

namespace API_Bloodborne.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private TokenService _tokenService;

        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task Cadastra(CreateUsuarioDto usuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);

            IdentityResult resultado = await _userManager.CreateAsync(usuario, usuarioDto.Password);
            Console.WriteLine(resultado);
            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuário!");
            }
        }

        public async Task<string> Login(LoginUsuarioDto loginDto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }

            var usuario = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == loginDto.Username.ToUpper());
            var token = _tokenService.GenerateToken(usuario);

            return token;
        }

        public async Task Deletar(DeleteUsuarioDto deleteDto)
        {
            var resultado1 = await _signInManager.PasswordSignInAsync(deleteDto.Username, deleteDto.Password, false, false);

            if (!resultado1.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }

            var usuario = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == deleteDto.Username.ToUpper());

            if (usuario == null)
            {
                throw new ApplicationException("Usuário não encontrado!");
            }

            var resultado = await _userManager.DeleteAsync(usuario);

            Console.WriteLine(resultado);
            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Falha ao deletar usuário!");
            }
        }
    }
}
