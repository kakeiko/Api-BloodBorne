using API_Bloodborne.Data;
using API_Bloodborne.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using API_Bloodborne.Data.DTOs.Itens;
using Microsoft.AspNetCore.Authorization;

namespace API_Bloodborne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly BloodborneContext _context;

        private IMapper _mapper;

        public ItemController(BloodborneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]

        public IActionResult AdicionarItem([FromBody] CreateItemDto itemDto)
        {
            Item item = _mapper.Map<Item>(itemDto);
            var lastId = _context.Itens
                    .OrderByDescending(i => i.Id)
                    .Select(i => i.Id)
                    .FirstOrDefault();

            if (lastId == null)
            {
                item.Id = $"IT01";
            }
            else
            {
                int newIdNumber = int.Parse(lastId.Substring(2)) + 1;
                item.Id = $"IT0{newIdNumber}";
            }
            _context.Itens.Add(item);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PegarItemPorId),
                new { id = item.Id },
                item);
        }

        [HttpGet]

        public IEnumerable<ReadItemDto> PegarItem()
        {
            return _mapper.Map<List<ReadItemDto>>(_context.Itens.Skip(0).Take(10));
        }

        [HttpGet("{id}")]
        public IActionResult PegarItemPorId(string id)
        {
            var item = _context.Itens.FirstOrDefault(item => item.Id == id);

            if (item == null) return NotFound();

            var itemDto = _mapper.Map<ReadItemDto>(item);

            return Ok(itemDto);
        }

        [HttpPut("{id}")]
        [Authorize]

        public IActionResult AtualizaItem(string id, [FromBody] UpdateItemDto itemDto)
        {
            var item = _context.Itens.FirstOrDefault(item => item.Id == id);

            if (item == null) return NotFound();
            _mapper.Map(itemDto, item);

            _context.SaveChanges();

            return Ok(item);
        }

        [HttpPatch("{id}")]
        [Authorize]

        public IActionResult AtualizaParcialItem(string id, JsonPatchDocument<UpdateItemDto> patch)
        {
            var item = _context.Itens.FirstOrDefault(item => item.Id == id);

            if (item == null) return NotFound();

            var itemParaAtualizar = _mapper.Map<UpdateItemDto>(item);

            patch.ApplyTo(itemParaAtualizar, ModelState);

            if (!TryValidateModel(itemParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(itemParaAtualizar, item);
            _context.SaveChanges();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        [Authorize]

        public IActionResult DeletarItem(string id)
        {
            var item = _context.Itens.FirstOrDefault(item => item.Id == id);

            if (item == null) return NotFound();

            _context.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
