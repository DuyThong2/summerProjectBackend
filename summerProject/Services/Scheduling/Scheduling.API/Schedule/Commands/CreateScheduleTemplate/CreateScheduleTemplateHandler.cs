namespace Scheduling.API.Schedule.Commands.CreateScheduleTemplate
{
    public class CreateScheduleTemplateHandler(
        IGenericRepository<ScheduleTemplate> templateRepo
    ) : ICommandHandler<CreateScheduleTemplateCommand, CreateScheduleTemplateResult>
    {
        public async Task<CreateScheduleTemplateResult> Handle(CreateScheduleTemplateCommand cmd, CancellationToken ct)
        {
            var entity = new ScheduleTemplate
            {
                Id = Guid.NewGuid(),
                Name = cmd.Template.Name,
                Description = cmd.Template.Description,
                TemplateDetails = cmd.Template.Details.Select(d => new ScheduleTemplateDetail
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = d.DayOfWeek,
                    TimeSlot = d.TimeSlot,
                    MealId = d.MealId
                }).ToList()
            };

            await templateRepo.AddAsync(entity, ct);
            await templateRepo.SaveChangesAsync(ct);

            return new CreateScheduleTemplateResult(entity.Id);
        }
    }

}
