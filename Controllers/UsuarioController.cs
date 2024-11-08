using API_Bloodborne.Data;
using API_Bloodborne.Data.DTOs.Usuarios;
using API_Bloodborne.Models;
using API_Bloodborne.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_Bloodborne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpPost("cadastro")]

        public async Task<IActionResult> CadastrarUsuario(CreateUsuarioDto usuarioDto)
        {
            await _usuarioService.Cadastra(usuarioDto);
            return Ok("Usuário cadastrado!");

        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginUsuarioDto loginDto)
        {
            var token = await _usuarioService.Login(loginDto);
            return Ok(token);
        }

        [HttpDelete("deletar")]

        public async Task<IActionResult> Deletar(DeleteUsuarioDto deletaDto)
        {
            await _usuarioService.Deletar(deletaDto);
            return Ok("Usuário deletedo!");
        }
    }
}
