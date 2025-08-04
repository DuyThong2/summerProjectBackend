using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Commands.UpdateScheduleTemplate
{
    public class UpdateScheduleTemplateHandler(
    IScheduleTemplateRepository templateRepo,    // có GetWithDetailsAsync
    IGenericRepository<ScheduleTemplateDetail> detailRepo
) : ICommandHandler<UpdateScheduleTemplateCommand, UpdateScheduleTemplateResult>
    {
        public async Task<UpdateScheduleTemplateResult> Handle(UpdateScheduleTemplateCommand cmd, CancellationToken ct)
        {
            if (cmd.Template.Id is null) throw new ArgumentException("Template Id is required");

            var entity = await templateRepo.GetWithDetailsAsync(cmd.Template.Id.Value);
            if (entity == null) throw new KeyNotFoundException("Template not found");

            entity.Name = cmd.Template.Name;
            entity.Description = cmd.Template.Description;

            var dtoDetails = cmd.Template.Details ?? new List<ScheduleTemplateDetailDto>();
            var dtoIds = dtoDetails.Where(d => d.Id.HasValue).Select(d => d.Id!.Value).ToHashSet();

            // delete removed
            var toRemove = entity.TemplateDetails.Where(d => !dtoIds.Contains(d.Id)).ToList();
            foreach (var r in toRemove) detailRepo.Delete(r);

            // add / update
            foreach (var d in dtoDetails)
            {
                if (d.Id.HasValue)
                {
                    var existing = entity.TemplateDetails.First(x => x.Id == d.Id.Value);
                    existing.DayOfWeek = d.DayOfWeek;
                    existing.TimeSlot = d.TimeSlot;
                    existing.MealId = d.MealId;
                }
                else
                {
                    entity.TemplateDetails.Add(new ScheduleTemplateDetail
                    {
                        Id = Guid.NewGuid(),
                        TemplateId = entity.Id,
                        DayOfWeek = d.DayOfWeek,
                        TimeSlot = d.TimeSlot,
                        MealId = d.MealId
                    });
                }
            }

            templateRepo.Update(entity);
            var ok = await templateRepo.SaveChangesAsync(ct);

            return new UpdateScheduleTemplateResult(ok);
        }
    }

}
