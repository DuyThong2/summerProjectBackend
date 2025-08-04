using FluentValidation;

namespace Scheduling.API.Schedule.Commands.UpdateMonthlyScheduleInstance
{
    public record UpdateMonthlyScheduleInstanceCommand(
    Guid Id,
    int Year,
    int Month,
    Guid? AppliedTemplateId
) : ICommand<UpdateMonthlyScheduleInstanceResult>;

    public record UpdateMonthlyScheduleInstanceResult(bool IsSuccess);

    public class UpdateMonthlyScheduleInstanceValidator : AbstractValidator<UpdateMonthlyScheduleInstanceCommand>
    {
        public UpdateMonthlyScheduleInstanceValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Year).GreaterThan(2000);
            RuleFor(x => x.Month).InclusiveBetween(1, 12);
        }
    }

}
