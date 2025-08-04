using Scheduling.API.Models.Materialized;

namespace Scheduling.API.Schedule.Commands.UpdateMonthlyScheduleItem
{
    public class UpdateMonthlyScheduleItemHandler(
    IGenericRepository<MonthlyScheduleItem> itemRepo
) : ICommandHandler<UpdateMonthlyScheduleItemCommand, UpdateMonthlyScheduleItemResult>
    {
        public async Task<UpdateMonthlyScheduleItemResult> Handle(UpdateMonthlyScheduleItemCommand cmd, CancellationToken ct)
        {
            var item = await itemRepo.GetByIdAsync(cmd.Id);
            if (item == null) throw new KeyNotFoundException("Item not found");

            item.Date = cmd.Item.Date;
            item.TimeSlot = cmd.Item.TimeSlot;
            item.MealId = cmd.Item.MealId;
            item.Source = cmd.Item.Source;
            item.SourceId = cmd.Item.SourceId;

            itemRepo.Update(item);
            var ok = await itemRepo.SaveChangesAsync();

            return new UpdateMonthlyScheduleItemResult(ok);
        }
    }

}
