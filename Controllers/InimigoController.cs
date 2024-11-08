using API_Bloodborne.Data;
using API_Bloodborne.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using API_Bloodborne.Data.DTOs.Inimigos;
using Microsoft.AspNetCore.Authorization;

namespace API_Bloodborne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InimigoController : ControllerBase
    {
        private readonly BloodborneContext _context;

        private IMapper _mapper;

        public InimigoController(BloodborneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]

        public IActionResult AdicionarInimigo([FromBody] CreateInimigoDto inimigoDto)
        {
            Inimigo inimigo = _mapper.Map<Inimigo>(inimigoDto);
            var lastId = _context.Inimigos
                    .OrderByDescending(i => i.Id)
                    .Select(i => i.Id)
                    .FirstOrDefault();

            if (lastId == null)
            {
                inimigo.Id = $"I01";
            }
            else
            {
                int newIdNumber = int.Parse(lastId.Substring(2)) + 1;
                inimigo.Id = $"I0{newIdNumber}";
            }
            _context.Inimigos.Add(inimigo);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PegarInimigoPorId),
                new { id = inimigo.Id },
                inimigo);
        }

        [HttpGet]

        public IEnumerable<ReadInimigoDto> PegarInimigo()
        {
            return _mapper.Map<List<ReadInimigoDto>>(_context.Inimigos.Skip(0).Take(10));
        }

        [HttpGet("{id}")]
        public IActionResult PegarInimigoPorId(string id)
        {
            var inimigo = _context.Inimigos.FirstOrDefault(inimigo => inimigo.Id == id);

            if (inimigo == null) return NotFound();

            var inimigoDto = _mapper.Map<ReadInimigoDto>(inimigo);

            return Ok(inimigoDto);
        }

        [HttpPut("{id}")]
        [Authorize]

        public IActionResult AtualizaInimigo(string id, [FromBody] UpdateInimigoDto inimigoDto)
        {
            var inimigo = _context.Inimigos.FirstOrDefault(inimigo => inimigo.Id == id);

            if (inimigo == null) return NotFound();
            _mapper.Map(inimigoDto, inimigo);

            _context.SaveChanges();

            return Ok(inimigo);
        }

        [HttpPatch("{id}")]
        [Authorize]

        public IActionResult AtualizaParcialInimigo(string id, JsonPatchDocument<UpdateInimigoDto> patch)
        {
            var inimigo = _context.Inimigos.FirstOrDefault(inimigo => inimigo.Id == id);

            if (inimigo == null) return NotFound();

            var inimigoParaAtualizar = _mapper.Map<UpdateInimigoDto>(inimigo);

            patch.ApplyTo(inimigoParaAtualizar, ModelState);

            if (!TryValidateModel(inimigoParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(inimigoParaAtualizar, inimigo);
            _context.SaveChanges();

            return Ok(inimigo);
        }

        [HttpDelete("{id}")]
        [Authorize]

        public IActionResult DeletarInimigo(string id)
        {
            var inimigo = _context.Inimigos.FirstOrDefault(inimigo => inimigo.Id == id);

            if (inimigo == null) return NotFound();

            _context.Remove(inimigo);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
