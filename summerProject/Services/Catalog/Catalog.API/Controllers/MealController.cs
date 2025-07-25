using Catalog.API.Command.Meal;
using Catalog.API.Queries.Meal;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BuildingBlocks.Pagination;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await mediator.Send(new GetMealByIdQuery(id));
            if (result.Meal == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var pagination = new PaginationRequest(pageIndex, pageSize);
            var result = await mediator.Send(new GetMealByNameQuery(name, pagination));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var pagination = new PaginationRequest(pageIndex, pageSize);
            var result = await mediator.Send(new GetMealQuery(pagination));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMealCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateMealCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            var result = await mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await mediator.Send(new DeleteMealCommand(id));
            return result ? NoContent() : NotFound();
        }


       

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("MealsController is alive!");
        }
    }
}
