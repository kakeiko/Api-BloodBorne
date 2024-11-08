using API_Bloodborne.Data;
using API_Bloodborne.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using API_Bloodborne.Data.DTOs.Locais;
using Microsoft.AspNetCore.Authorization;

namespace API_Bloodborne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalController : ControllerBase
    {
        private readonly BloodborneContext _context;

        private IMapper _mapper;

        public LocalController(BloodborneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]

        public IActionResult AdicionarLocal([FromBody] CreateLocalDto localDto)
        {
            Local local = _mapper.Map<Local>(localDto);
            var lastId = _context.Locais
                    .OrderByDescending(l => l.Id)
                    .Select(l => l.Id)
                    .FirstOrDefault();

            if (lastId == null)
            {
                local.Id = $"L01";
            }
            else
            {
                int newIdNumber = int.Parse(lastId.Substring(2)) + 1;
                local.Id = $"L0{newIdNumber}";
            }
            _context.Locais.Add(local);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PegarLocalPorId),
                new { id = local.Id },
                local);
        }

        [HttpGet]

        public IEnumerable<ReadLocalDto> PegarLocal()
        {
            return _mapper.Map<List<ReadLocalDto>>(_context.Locais.Skip(0).Take(10));
        }

        [HttpGet("{id}")]
        public IActionResult PegarLocalPorId(string id)
        {
            var local = _context.Locais.FirstOrDefault(local => local.Id == id);

            if (local == null) return NotFound();

            var localDto = _mapper.Map<ReadLocalDto>(local);

            return Ok(localDto);
        }

        [HttpPut("{id}")]
        [Authorize]

        public IActionResult AtualizaLocal(string id, [FromBody] UpdateLocalDto localDto)
        {
            var local = _context.Locais.FirstOrDefault(local => local.Id == id);

            if (local == null) return NotFound();
            _mapper.Map(localDto, local);

            _context.SaveChanges();

            return Ok(local);
        }

        [HttpPatch("{id}")]
        [Authorize]

        public IActionResult AtualizaParcialLocal(string id, JsonPatchDocument<UpdateLocalDto> patch)
        {
            var local = _context.Locais.FirstOrDefault(local => local.Id == id);

            if (local == null) return NotFound();

            var localParaAtualizar = _mapper.Map<UpdateLocalDto>(local);

            patch.ApplyTo(localParaAtualizar, ModelState);

            if (!TryValidateModel(localParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(localParaAtualizar, local);
            _context.SaveChanges();

            return Ok(local);
        }

        [HttpDelete("{id}")]
        [Authorize]

        public IActionResult DeletarLocal(string id)
        {
            var local = _context.Locais.FirstOrDefault(local => local.Id == id);

            if (local == null) return NotFound();

            _context.Remove(local);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
