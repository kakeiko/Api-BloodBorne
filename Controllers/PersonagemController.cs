using API_Bloodborne.Data;
using API_Bloodborne.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using API_Bloodborne.Data.DTOs.Personagens;
using Microsoft.AspNetCore.Authorization;

namespace API_Bloodborne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonagemController : ControllerBase
    {
        private readonly BloodborneContext _context;

        private IMapper _mapper;

        public PersonagemController(BloodborneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]

        public IActionResult AdicionarPersonagem([FromBody] CreatePersonagemDto personagemDto)
        {
            Personagem personagem = _mapper.Map<Personagem>(personagemDto);
            var lastId = _context.Personagens
                    .OrderByDescending(p => p.Id)
                    .Select(p => p.Id)
                    .FirstOrDefault();

            if (lastId == null)
            {
                personagem.Id = $"P01";
            }
            else
            {
                int newIdNumber = int.Parse(lastId.Substring(2)) + 1;
                personagem.Id = $"P0{newIdNumber}";
            }
            _context.Personagens.Add(personagem);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PegarPersonagemPorId),
                new { id = personagem.Id },
                personagem);
        }

        [HttpGet]

        public IEnumerable<ReadPersonagemDto> PegarPersonagem()
        {
            return _mapper.Map<List<ReadPersonagemDto>>(_context.Personagens.Skip(0).Take(10));
        }

        [HttpGet("{id}")]
        public IActionResult PegarPersonagemPorId(string id)
        {
            var personagem = _context.Personagens.FirstOrDefault(personagem => personagem.Id == id);

            if (personagem == null) return NotFound();

            var personagemDto = _mapper.Map<ReadPersonagemDto>(personagem);

            return Ok(personagemDto);
        }

        [HttpPut("{id}")]
        [Authorize]

        public IActionResult AtualizaPersonagem(string id, [FromBody] UpdatePersonagemDto personagemDto)
        {
            var personagem = _context.Personagens.FirstOrDefault(personagem => personagem.Id == id);

            if (personagem == null) return NotFound();
            _mapper.Map(personagemDto, personagem);

            _context.SaveChanges();

            return Ok(personagem);
        }

        [HttpPatch("{id}")]
        [Authorize]

        public IActionResult AtualizaParcialPersonagem(string id, JsonPatchDocument<UpdatePersonagemDto> patch)
        {
            var personagem = _context.Personagens.FirstOrDefault(personagem => personagem.Id == id);

            if (personagem == null) return NotFound();

            var personagemParaAtualizar = _mapper.Map<UpdatePersonagemDto>(personagem);

            patch.ApplyTo(personagemParaAtualizar, ModelState);

            if (!TryValidateModel(personagemParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(personagemParaAtualizar, personagem);
            _context.SaveChanges();

            return Ok(personagem);
        }

        [HttpDelete("{id}")]
        [Authorize]

        public IActionResult DeletarPersonagem(string id)
        {
            var personagem = _context.Personagens.FirstOrDefault(personagem => personagem.Id == id);

            if (personagem == null) return NotFound();

            _context.Remove(personagem);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
