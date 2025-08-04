namespace Scheduling.API.Schedule.Commands.DeleteScheduleTemplate
{
    public class DeleteScheduleTemplateHandler(
    IGenericRepository<ScheduleTemplate> templateRepo
) : ICommandHandler<DeleteScheduleTemplateCommand, DeleteScheduleTemplateResult>
    {
        public async Task<DeleteScheduleTemplateResult> Handle(DeleteScheduleTemplateCommand cmd, CancellationToken ct)
        {
            var entity = await templateRepo.GetByIdAsync(cmd.Id, ct);
            if (entity == null) return new DeleteScheduleTemplateResult(false);

            templateRepo.Delete(entity);
            var ok = await templateRepo.SaveChangesAsync(ct);
            return new DeleteScheduleTemplateResult(ok);
        }
    }

}
