using Scheduling.API.Enums.Materialized;
using Scheduling.API.Models.Materialized;

namespace Scheduling.API.Schedule.Commands.CreateMonthlyScheduleInstanceFromTemplate
{
    public class CreateMonthlyScheduleInstanceFromTemplateHandler(
    IScheduleTemplateRepository templateRepo,
    IGenericRepository<MonthlyScheduleInstance> instanceRepo,
    IGenericRepository<MonthlyScheduleItem> itemRepo
) : ICommandHandler<CreateMonthlyScheduleInstanceFromTemplateCommand, CreateMonthlyScheduleInstanceFromTemplateResult>
    {
        public async Task<CreateMonthlyScheduleInstanceFromTemplateResult> Handle(CreateMonthlyScheduleInstanceFromTemplateCommand cmd, CancellationToken ct)
        {
            var template = await templateRepo.GetWithDetailsAsync(cmd.TemplateId);
            if (template == null) throw new KeyNotFoundException("Template not found");

            // Kiểm tra instance tồn tại
            var all = await instanceRepo.GetAllAsync(ct);
            var existed = all.FirstOrDefault(x =>
                x.ScheduleCollectionId == cmd.ScheduleCollectionId &&
                x.Year == cmd.Year &&
                x.Month == cmd.Month);

            if (existed != null)
            {
                if (!cmd.OverwriteIfExists)
                    throw new InvalidOperationException("Monthly schedule already exists for this collection and month.");

                // Xoá cũ
                instanceRepo.Delete(existed);
                await instanceRepo.SaveChangesAsync(ct);
            }

            var instance = new MonthlyScheduleInstance
            {
                Id = Guid.NewGuid(),
                ScheduleCollectionId = cmd.ScheduleCollectionId,
                Year = cmd.Year,
                Month = cmd.Month,
                AppliedTemplateId = template.Id,
                GeneratedAt = DateTime.UtcNow
            };

            await instanceRepo.AddAsync(instance, ct);
            await instanceRepo.SaveChangesAsync(ct);

            // build items
            var daysInMonth = DaysInMonth(cmd.Year, cmd.Month);
            int count = 0;

            foreach (var day in daysInMonth)
            {
                var dow = day.DayOfWeek;
                var matches = template.TemplateDetails.Where(d => d.DayOfWeek == dow);
                foreach (var d in matches)
                {
                    var item = new MonthlyScheduleItem
                    {
                        Id = Guid.NewGuid(),
                        MonthlyScheduleInstanceId = instance.Id,
                        Date = day,
                        TimeSlot = d.TimeSlot,
                        MealId = d.MealId,
                        Source = ScheduleItemSource.Template,
                        SourceId = d.Id
                    };
                    await itemRepo.AddAsync(item, ct);
                    count++;
                }
            }

            await itemRepo.SaveChangesAsync(ct);

            return new CreateMonthlyScheduleInstanceFromTemplateResult(instance.Id, count);
        }

        private static IEnumerable<DateTime> DaysInMonth(int year, int month)
        {
            var start = new DateTime(year, month, 1);
            var end = start.AddMonths(1).AddDays(-1);
            for (var d = start; d <= end; d = d.AddDays(1))
                yield return d;
        }
    }

}
