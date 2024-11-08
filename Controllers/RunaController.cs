using API_Bloodborne.Data;
using API_Bloodborne.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using API_Bloodborne.Data.DTOs.Runas;
using Microsoft.AspNetCore.Authorization;

namespace API_Bloodborne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunaController : ControllerBase
    {
        private readonly BloodborneContext _context;

        private IMapper _mapper;

        public RunaController(BloodborneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]

        public IActionResult AdicionarRuna([FromBody] CreateRunaDto runaDto)
        {
            Runa runa = _mapper.Map<Runa>(runaDto);
            var lastId = _context.Runas
                    .OrderByDescending(r => r.Id)
                    .Select(r => r.Id)
                    .FirstOrDefault();

            if (lastId == null)
            {
                runa.Id = $"RU01";
            }
            else
            {
                int newIdNumber = int.Parse(lastId.Substring(2)) + 1;
                runa.Id = $"RU0{newIdNumber}";
            }
            _context.Runas.Add(runa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PegarRunaPorId),
                new { id = runa.Id },
                runa);
        }

        [HttpGet]

        public IEnumerable<ReadRunaDto> PegarRuna()
        {
            return _mapper.Map<List<ReadRunaDto>>(_context.Runas.Skip(0).Take(10));
        }

        [HttpGet("{id}")]
        public IActionResult PegarRunaPorId(string id)
        {
            var runa = _context.Runas.FirstOrDefault(runa => runa.Id == id);

            if (runa == null) return NotFound();

            var runaDto = _mapper.Map<ReadRunaDto>(runa);

            return Ok(runaDto);
        }

        [HttpPut("{id}")]
        [Authorize]

        public IActionResult AtualizaRuna(string id, [FromBody] UpdateRunaDto runaDto)
        {
            var runa = _context.Runas.FirstOrDefault(runa => runa.Id == id);

            if (runa == null) return NotFound();
            _mapper.Map(runaDto, runa);

            _context.SaveChanges();

            return Ok(runa);
        }

        [HttpPatch("{id}")]
        [Authorize]

        public IActionResult AtualizaParcialRuna(string id, JsonPatchDocument<UpdateRunaDto> patch)
        {
            var runa = _context.Runas.FirstOrDefault(runa => runa.Id == id);

            if (runa == null) return NotFound();

            var runaParaAtualizar = _mapper.Map<UpdateRunaDto>(runa);

            patch.ApplyTo(runaParaAtualizar, ModelState);

            if (!TryValidateModel(runaParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(runaParaAtualizar, runa);
            _context.SaveChanges();

            return Ok(runa);
        }

        [HttpDelete("{id}")]
        [Authorize]

        public IActionResult DeletarRuna(string id)
        {
            var runa = _context.Runas.FirstOrDefault(runa => runa.Id == id);

            if (runa == null) return NotFound();

            _context.Remove(runa);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
