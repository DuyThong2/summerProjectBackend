using Scheduling.API.Models.Materialized;

namespace Scheduling.API.Schedule.Commands.CreateMonthlyScheduleInstance
{
    public class CreateMonthlyScheduleInstanceHandler(
            IGenericRepository<MonthlyScheduleInstance> instanceRepo
        ) : ICommandHandler<CreateMonthlyScheduleInstanceCommand, CreateMonthlyScheduleInstanceResult>
    {
        public async Task<CreateMonthlyScheduleInstanceResult> Handle(CreateMonthlyScheduleInstanceCommand cmd, CancellationToken ct)
        {
            // (Tuỳ bạn) kiểm tra tồn tại (ScheduleCollectionId, Year, Month) -> Unique
            var entity = new MonthlyScheduleInstance
            {
                Id = Guid.NewGuid(),
                ScheduleCollectionId = cmd.ScheduleCollectionId,
                Year = cmd.Year,
                Month = cmd.Month,
                AppliedTemplateId = cmd.AppliedTemplateId,
                GeneratedAt = DateTime.UtcNow
            };

            await instanceRepo.AddAsync(entity);
            await instanceRepo.SaveChangesAsync();

            return new CreateMonthlyScheduleInstanceResult(entity.Id);
        }
    }

}
