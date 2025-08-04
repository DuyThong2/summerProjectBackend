using Scheduling.API.Models.Materialized;

namespace Scheduling.API.Schedule.Commands.DeleteMonthlyScheduleInstance
{
    public class DeleteMonthlyScheduleInstanceHandler(
    IGenericRepository<MonthlyScheduleInstance> instanceRepo
) : ICommandHandler<DeleteMonthlyScheduleInstanceCommand, bool>
    {
        public async Task<bool> Handle(DeleteMonthlyScheduleInstanceCommand cmd, CancellationToken ct)
        {
            var instance = await instanceRepo.GetByIdAsync(cmd.Id);
            if (instance == null) return false;

            instanceRepo.Delete(instance);
            return await instanceRepo.SaveChangesAsync();
        }
    }

}
