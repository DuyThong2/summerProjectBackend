using Scheduling.API.Models.Materialized;

namespace Scheduling.API.Schedule.Commands.AddMonthlyScheduleItemsBulk
{
    public class AddMonthlyScheduleItemsBulkHandler(
        IGenericRepository<MonthlyScheduleItem> itemRepo
    ) : ICommandHandler<AddMonthlyScheduleItemsBulkCommand, AddMonthlyScheduleItemsBulkResult>
    {
        public async Task<AddMonthlyScheduleItemsBulkResult> Handle(AddMonthlyScheduleItemsBulkCommand cmd, CancellationToken ct)
        {
            foreach (var dto in cmd.Items)
            {
                var item = new MonthlyScheduleItem
                {
                    Id = Guid.NewGuid(),
                    MonthlyScheduleInstanceId = cmd.MonthlyInstanceId,
                    Date = dto.Date,
                    TimeSlot = dto.TimeSlot,
                    MealId = dto.MealId,
                    Source = dto.Source,
                    SourceId = dto.SourceId
                };
                await itemRepo.AddAsync(item);
            }

            var ok = await itemRepo.SaveChangesAsync();
            return new AddMonthlyScheduleItemsBulkResult(ok ? cmd.Items.Count : 0);
        }
    }

}
