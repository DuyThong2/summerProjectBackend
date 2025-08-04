using Scheduling.API.Models.Materialized;
using Scheduling.API.Schedule.Commands.NewFolder;

namespace Scheduling.API.Schedule.Commands.CreateMonthlyScheduleItem
{
    public class CreateMonthlyScheduleItemHandler(
        IGenericRepository<MonthlyScheduleItem> itemRepo
    ) : ICommandHandler<CreateMonthlyScheduleItemCommand, CreateMonthlyScheduleItemResult>
    {
        public async Task<CreateMonthlyScheduleItemResult> Handle(CreateMonthlyScheduleItemCommand cmd, CancellationToken ct)
        {
            var item = new MonthlyScheduleItem
            {
                Id = Guid.NewGuid(),
                MonthlyScheduleInstanceId = cmd.MonthlyInstanceId,
                Date = cmd.Item.Date,
                TimeSlot = cmd.Item.TimeSlot,
                MealId = cmd.Item.MealId,
                Source = cmd.Item.Source,
                SourceId = cmd.Item.SourceId
            };

            await itemRepo.AddAsync(item);
            await itemRepo.SaveChangesAsync();

            return new CreateMonthlyScheduleItemResult(item.Id);
        }
    }

}
