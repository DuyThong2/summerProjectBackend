using FluentValidation;

namespace Scheduling.API.Schedule.Commands.DeleteScheduleTemplate

{
    public record DeleteScheduleTemplateCommand(Guid Id)
    : ICommand<DeleteScheduleTemplateResult>;

    public record DeleteScheduleTemplateResult(bool IsSuccess);

    public class DeleteScheduleTemplateValidator : AbstractValidator<DeleteScheduleTemplateCommand>
    {
        public DeleteScheduleTemplateValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}
