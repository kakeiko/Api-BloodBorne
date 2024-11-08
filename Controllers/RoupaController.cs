using API_Bloodborne.Data;
using API_Bloodborne.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using API_Bloodborne.Data.DTOs.Roupas;
using Microsoft.AspNetCore.Authorization;

namespace API_Bloodborne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoupaController : ControllerBase
    {
        private readonly BloodborneContext _context;

        private IMapper _mapper;

        public RoupaController(BloodborneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]

        public IActionResult AdicionarRoupa([FromBody] CreateRoupaDto roupaDto)
        {
            Roupa roupa = _mapper.Map<Roupa>(roupaDto);
            var lastId = _context.Roupas
                    .OrderByDescending(p => p.Id)
                    .Select(p => p.Id)
                    .FirstOrDefault();

            if (lastId == null)
            {
                roupa.Id = $"R01";
            }
            else
            {
                int newIdNumber = int.Parse(lastId.Substring(2)) + 1;
                roupa.Id = $"R0{newIdNumber}";
            }
            _context.Roupas.Add(roupa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PegarRoupaPorId),
                new { id = roupa.Id },
                roupa);
        }

        [HttpGet]

        public IEnumerable<ReadRoupaDto> PegarRoupa()
        {
            return _mapper.Map<List<ReadRoupaDto>>(_context.Roupas.Skip(0).Take(10));
        }

        [HttpGet("{id}")]
        public IActionResult PegarRoupaPorId(string id)
        {
            var roupa = _context.Roupas.FirstOrDefault(roupa => roupa.Id == id);

            if (roupa == null) return NotFound();

            var roupaDto = _mapper.Map<ReadRoupaDto>(roupa);

            return Ok(roupaDto);
        }

        [HttpPut("{id}")]
        [Authorize]

        public IActionResult AtualizaRoupa(string id, [FromBody] UpdateRoupaDto roupaDto)
        {
            var roupa = _context.Roupas.FirstOrDefault(roupa => roupa.Id == id);

            if (roupa == null) return NotFound();
            _mapper.Map(roupaDto, roupa);

            _context.SaveChanges();

            return Ok(roupa);
        }

        [HttpPatch("{id}")]
        [Authorize]

        public IActionResult AtualizaParcialRoupa(string id, JsonPatchDocument<UpdateRoupaDto> patch)
        {
            var roupa = _context.Roupas.FirstOrDefault(roupa => roupa.Id == id);

            if (roupa == null) return NotFound();

            var roupaParaAtualizar = _mapper.Map<UpdateRoupaDto>(roupa);

            patch.ApplyTo(roupaParaAtualizar, ModelState);

            if (!TryValidateModel(roupaParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(roupaParaAtualizar, roupa);
            _context.SaveChanges();

            return Ok(roupa);
        }

        [HttpDelete("{id}")]
        [Authorize]

        public IActionResult DeletarRoupa(string id)
        {
            var roupa = _context.Roupas.FirstOrDefault(roupa => roupa.Id == id);

            if (roupa == null) return NotFound();

            _context.Remove(roupa);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
