using AutoMapper;
using Catalog.API.Dtos.Ingredient;
using Catalog.API.Models;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
        private readonly IMapper _mapper;

        public IngredientsController(IIngredientService ingredientService, IMapper mapper)
        {
            _ingredientService = ingredientService;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _ingredientService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IngredientAddDTO ingredientToAdd)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientToAdd);
            await _ingredientService.AddAsync(ingredient);
            return CreatedAtAction(nameof(GetById), new { id = ingredient.Id }, ingredient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Ingredient ingredient)
        {
            if (id != ingredient.Id)
                return BadRequest("ID mismatch");

            var success = await _ingredientService.UpdateAsync(id, ingredient);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _ingredientService.RemoveAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string? term, int page = 1, int pageSize = 10)
        {
            var (items, totalCount) = await _ingredientService.GetPagedAsync(term, page, pageSize);

            return Ok(new
            {
                pageIndex = page,
                pageSize = pageSize,
                count = totalCount,
                data = items
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var (items, totalCount) = await _ingredientService.GetPagedAsync(pageIndex, pageSize);
            return Ok(new
            {
                pageIndex,
                pageSize,
                count = totalCount,
                data = items
            });
        }

    }
}
