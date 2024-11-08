using API_Bloodborne.Data;
using API_Bloodborne.Data.DTOs.Armas;
using API_Bloodborne.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API_Bloodborne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArmaController : ControllerBase
    {

        private readonly BloodborneContext _context;

        private IMapper _mapper;

        public ArmaController(BloodborneContext context, IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AdicionarArma([FromBody] CreateArmaDto armaDto)
        {
            Arma arma = _mapper.Map<Arma>(armaDto);
            var lastId = _context.Armas
                    .OrderByDescending(a => a.Id)
                    .Select(a => a.Id)
                    .FirstOrDefault();
            if (lastId == null)
            {
                arma.Id = $"A01";
            }
            else
            {
                int newIdNumber = int.Parse(lastId.Substring(2)) + 1;
                arma.Id = $"A0{newIdNumber}";
            }    
            _context.Armas.Add(arma);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PegarArmaPorId),
                new { id = arma.Id },
                arma);
        }

        [HttpGet]
        
        public IEnumerable<ReadArmaDto> PegarArma()
        {
            return _mapper.Map<List<ReadArmaDto>>(_context.Armas.Skip(0).Take(10));
        }

        [HttpGet("{id}")]
        public IActionResult PegarArmaPorId(string id)
        {
            var arma = _context.Armas.FirstOrDefault(arma => arma.Id == id);

            if (arma == null) return NotFound();

            var armaDto = _mapper.Map<ReadArmaDto>(arma);

            return Ok(armaDto);
        }

        [HttpPut("{id}")]
        [Authorize]

        public IActionResult AtualizaArma(string id, [FromBody] UpdateArmaDto armaDto)
        {
            var arma = _context.Armas.FirstOrDefault(arma => arma.Id == id);

            if (arma == null) return NotFound();
            _mapper.Map(armaDto, arma);

            _context.SaveChanges();

            return Ok(arma);
        }

        [HttpPatch("{id}")]
        [Authorize]

        public IActionResult AtualizaParcialArma(string id, JsonPatchDocument<UpdateArmaDto> patch)
        {
            var arma = _context.Armas.FirstOrDefault(arma => arma.Id == id);

            if (arma == null) return NotFound();

            var armaParaAtualizar = _mapper.Map<UpdateArmaDto>(arma);

            patch.ApplyTo(armaParaAtualizar, ModelState);

            if (!TryValidateModel(armaParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(armaParaAtualizar, arma);
            _context.SaveChanges();

            return Ok(arma);
        }

        [HttpDelete("{id}")]
        [Authorize]

        public IActionResult DeletarArma(string id)
        {
            var arma = _context.Armas.FirstOrDefault(arma => arma.Id == id);

            if (arma == null) return NotFound();

            _context.Remove(arma);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
