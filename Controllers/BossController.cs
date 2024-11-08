using API_Bloodborne.Data;
using API_Bloodborne.Data.DTOs.Bosses;
using API_Bloodborne.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_Bloodborne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BossController : ControllerBase
    {
        private readonly BloodborneContext _context;

        private IMapper _mapper;

        public BossController(BloodborneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]

        public IActionResult AdicionarBoss([FromBody] CreateBossDto bossDto)
        {
            Boss boss = _mapper.Map<Boss>(bossDto);
            var lastId = _context.Bosses
                    .OrderByDescending(b => b.Id)
                    .Select(b => b.Id)
                    .FirstOrDefault();
            if (lastId == null)
            {
                boss.Id = $"B01";
            }
            else
            {
                int newIdNumber = int.Parse(lastId.Substring(2)) + 1;
                boss.Id = $"B0{newIdNumber}";
            }
            _context.Bosses.Add(boss);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PegarBossPorId),
                new { id = boss.Id },
                boss);
        }

        [HttpGet]

        public IEnumerable<ReadBossDto> PegarBoss()
        {
            return _mapper.Map<List<ReadBossDto>>(_context.Bosses.Skip(0).Take(10));
        }

        [HttpGet("{id}")]
        public IActionResult PegarBossPorId(string id)
        {
            var boss = _context.Bosses.FirstOrDefault(boss => boss.Id == id);

            if (boss == null) return NotFound();

            var bossDto = _mapper.Map<ReadBossDto>(boss);

            return Ok(bossDto);
        }

        [HttpPut("{id}")]
        [Authorize]

        public IActionResult AtualizaBoss(string id, [FromBody] UpdateBossDto bossDto)
        {
            var boss = _context.Bosses.FirstOrDefault(boss => boss.Id == id);

            if (boss == null) return NotFound();
            _mapper.Map(bossDto, boss);

            _context.SaveChanges();

            return Ok(boss);
        }

        [HttpPatch("{id}")]
        [Authorize]

        public IActionResult AtualizaParcialBoss(string id, JsonPatchDocument<UpdateBossDto> patch)
        {
            var boss = _context.Bosses.FirstOrDefault(boss => boss.Id == id);

            if (boss == null) return NotFound();

            var bossParaAtualizar = _mapper.Map<UpdateBossDto>(boss);

            patch.ApplyTo(bossParaAtualizar, ModelState);

            if (!TryValidateModel(bossParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(bossParaAtualizar, boss);
            _context.SaveChanges();

            return Ok(boss);
        }

        [HttpDelete("{id}")]
        [Authorize]

        public IActionResult DeletarArma(string id)
        {
            var boss = _context.Bosses.FirstOrDefault(boss => boss.Id == id);

            if (boss == null) return NotFound();

            _context.Remove(boss);
            _context.SaveChanges();
            return NoContent();
        }
    
    }
}
