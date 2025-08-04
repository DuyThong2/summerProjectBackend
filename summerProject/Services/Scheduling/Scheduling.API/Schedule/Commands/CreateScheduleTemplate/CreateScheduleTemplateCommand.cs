using FluentValidation;
using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Commands.CreateScheduleTemplate
{
    public record CreateScheduleTemplateCommand(ScheduleTemplateDto Template)
    : ICommand<CreateScheduleTemplateResult>;

    public record CreateScheduleTemplateResult(Guid Id);

    public class CreateScheduleTemplateValidator : AbstractValidator<CreateScheduleTemplateCommand>
    {
        public CreateScheduleTemplateValidator()
        {
            RuleFor(x => x.Template.Name).NotEmpty().MaximumLength(150);
            RuleForEach(x => x.Template.Details).ChildRules(d =>
            {
                d.RuleFor(x => x.MealId).NotEmpty();
            });
        }
    }

}
