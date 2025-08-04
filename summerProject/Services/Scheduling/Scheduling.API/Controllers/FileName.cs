using Microsoft.AspNetCore.Mvc;
using Scheduling.API.Dto;
using Scheduling.API.Schedule.Commands.AddMonthlyScheduleItemsBulk;
using Scheduling.API.Schedule.Commands.CreateMonthlyScheduleInstance;
using Scheduling.API.Schedule.Commands.CreateMonthlyScheduleInstanceFromTemplate;
using Scheduling.API.Schedule.Commands.NewFolder;
using Scheduling.API.Schedule.Commands.CreateScheduleTemplate;
using Scheduling.API.Schedule.Commands.DeleteMonthlyScheduleInstance;
using Scheduling.API.Schedule.Commands.UpdateMonthlyScheduleInstance;
using Scheduling.API.Schedule.Commands.UpdateMonthlyScheduleItem;
using Scheduling.API.Schedule.Commands.UpdateScheduleTemplate;
using Scheduling.API.Schedule.Queries;
using Scheduling.API.Schedule.Queries.GetCalendarRange;

namespace Scheduling.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SchedulingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-instance")]
        public async Task<IActionResult> CreateMonthlyInstance(
            [FromBody] CreateMonthlyScheduleInstanceCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("create-instance-from-template")]
        public async Task<IActionResult> CreateMonthlyInstanceFromTemplate(
            [FromBody] CreateMonthlyScheduleInstanceFromTemplateCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("bulk-items")]
        public async Task<IActionResult> AddMonthlyItemsBulk(
            [FromBody] AddMonthlyScheduleItemsBulkCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddMonthlyItem(
            [FromBody] CreateMonthlyScheduleItemCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("create-template")]
        public async Task<IActionResult> CreateScheduleTemplate(
            [FromBody] CreateScheduleTemplateCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("delete-instance/{id:guid}")]
        public async Task<IActionResult> DeleteMonthlyInstance(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteMonthlyScheduleInstanceCommand(id), cancellationToken);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("delete-item/{id:guid}")]
        public async Task<IActionResult> DeleteMonthlyItem(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteMonthlyScheduleItemCommand(id), cancellationToken);
            return result ? NoContent() : NotFound();
        }

        [HttpPut("update-instance")]
        public async Task<IActionResult> UpdateMonthlyInstance(
            [FromBody] UpdateMonthlyScheduleInstanceCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Cập nhật Monthly Schedule Item
        /// </summary>
        [HttpPut("update-item")]
        public async Task<IActionResult> UpdateMonthlyItem(
            [FromBody] UpdateMonthlyScheduleItemCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Cập nhật Schedule Template
        /// </summary>
        [HttpPut("update-template")]
        public async Task<IActionResult> UpdateScheduleTemplate(
            [FromBody] UpdateScheduleTemplateCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result) : NotFound();
        }


        [HttpGet("calendar")]
        public async Task<IActionResult> GetCalendar(
           [FromQuery] string userId,
           [FromQuery] DateTime from,
           [FromQuery] DateTime to,
           CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCalendarRangeQuery(userId, from, to), cancellationToken);
            return Ok(result);
        }

        [HttpGet("instance/{id:guid}")]
        public async Task<IActionResult> GetMonthlyInstanceById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetMonthlyScheduleInstanceByIdQuery(id), cancellationToken);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpGet("instance-by-month")]
        public async Task<IActionResult> GetMonthlyInstanceByMonth(
            [FromQuery] Guid scheduleCollectionId,
            [FromQuery] int year,
            [FromQuery] int month,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetMonthlyScheduleInstanceByMonthQuery(scheduleCollectionId, year, month), cancellationToken);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpGet("instances-by-user/{userId}")]
        public async Task<IActionResult> GetMonthlyInstancesByUser(
            string userId,
            [FromQuery] int? year,
            [FromQuery] int? month,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetMonthlyScheduleInstancesByUserQuery(userId, year, month), cancellationToken);
            return Ok(result);
        }

        [HttpGet("item/{id:guid}")]
        public async Task<IActionResult> GetMonthlyItemById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetMonthlyScheduleItemByIdQuery(id), cancellationToken);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpGet("items-by-user/{userId}")]
        public async Task<IActionResult> GetMonthlyItemsByUser(
            string userId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetMonthlyScheduleItemsByUserQuery(userId, from, to), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy items trong khoảng ngày (bắt buộc from-to)
        /// </summary>
        [HttpGet("items-range")]
        public async Task<IActionResult> GetMonthlyItemsRange(
            [FromQuery] string userId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetMonthlyScheduleItemsRangeQuery(userId, from, to), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Lấy Schedule Collection theo Id
        /// </summary>
        [HttpGet("collection/{id:guid}")]
        public async Task<IActionResult> GetScheduleCollectionById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetScheduleCollectionByIdQuery(id), cancellationToken);
            return result is not null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Lấy Schedule Template theo Id
        /// </summary>
        [HttpGet("template/{id:guid}")]
        public async Task<IActionResult> GetScheduleTemplateById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetScheduleTemplateByIdQuery(id), cancellationToken);
            return result is not null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Lấy danh sách tất cả template
        /// </summary>
        [HttpGet("templates")]
        public async Task<IActionResult> ListScheduleTemplates(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ListScheduleTemplatesQuery(), cancellationToken);
            return Ok(result);
        }



    }
}
