using FluentValidation;
using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Commands.UpdateScheduleTemplate
{
    public record UpdateScheduleTemplateCommand(ScheduleTemplateDto Template)
    : ICommand<UpdateScheduleTemplateResult>;

    public record UpdateScheduleTemplateResult(bool IsSuccess);

    public class UpdateScheduleTemplateValidator : AbstractValidator<UpdateScheduleTemplateCommand>
    {
        public UpdateScheduleTemplateValidator()
        {
            RuleFor(x => x.Template.Id).NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.Template.Name).NotEmpty().MaximumLength(150);
        }
    }

}
