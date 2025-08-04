using Scheduling.API.Models.Materialized;

namespace Scheduling.API.Schedule.Commands.NewFolder
{
    public class DeleteMonthlyScheduleItemHandler(
    IGenericRepository<MonthlyScheduleItem> itemRepo
) : ICommandHandler<DeleteMonthlyScheduleItemCommand, bool>
    {
        public async Task<bool> Handle(DeleteMonthlyScheduleItemCommand cmd, CancellationToken ct)
        {
            var item = await itemRepo.GetByIdAsync(cmd.Id);
            if (item == null) return false;
            itemRepo.Delete(item);
            return await itemRepo.SaveChangesAsync();
        }
    }

}
