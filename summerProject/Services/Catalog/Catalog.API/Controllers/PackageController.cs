
using Catalog.API.Command.Package;
using Catalog.API.Queries.PackageQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PackagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var result = await _mediator.Send(new GetPackagewithDetailByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePackageCommand command)
        {
            var newId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = newId }, new { id = newId });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePackageCommand command)
        {
            var success = await _mediator.Send(command);
            return success ? NoContent() : NotFound();
        }
    }
}