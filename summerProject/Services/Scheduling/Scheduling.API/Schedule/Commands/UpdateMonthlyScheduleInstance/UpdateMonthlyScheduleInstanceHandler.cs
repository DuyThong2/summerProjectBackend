using Scheduling.API.Models.Materialized;

namespace Scheduling.API.Schedule.Commands.UpdateMonthlyScheduleInstance
{
    public class UpdateMonthlyScheduleInstanceHandler(
    IGenericRepository<MonthlyScheduleInstance> instanceRepo
) : ICommandHandler<UpdateMonthlyScheduleInstanceCommand, UpdateMonthlyScheduleInstanceResult>
    {
        public async Task<UpdateMonthlyScheduleInstanceResult> Handle(UpdateMonthlyScheduleInstanceCommand cmd, CancellationToken ct)
        {
            var entity = await instanceRepo.GetByIdAsync(cmd.Id);
            if (entity == null) throw new KeyNotFoundException("Instance not found");

            entity.Year = cmd.Year;
            entity.Month = cmd.Month;
            entity.AppliedTemplateId = cmd.AppliedTemplateId;
            entity.GeneratedAt = DateTime.UtcNow;

            instanceRepo.Update(entity);
            var ok = await instanceRepo.SaveChangesAsync();

            return new UpdateMonthlyScheduleInstanceResult(ok);
        }
    }

}
