using FluentValidation;

namespace Scheduling.API.Schedule.Commands.CreateMonthlyScheduleInstance
{
    public record CreateMonthlyScheduleInstanceCommand(
        Guid ScheduleCollectionId,
        int Year,
        int Month,
        Guid? AppliedTemplateId
    ) : ICommand<CreateMonthlyScheduleInstanceResult>;

    public record CreateMonthlyScheduleInstanceResult(Guid Id);

    public class CreateMonthlyScheduleInstanceValidator : AbstractValidator<CreateMonthlyScheduleInstanceCommand>
    {
        public CreateMonthlyScheduleInstanceValidator()
        {
            RuleFor(x => x.ScheduleCollectionId).NotEmpty();
            RuleFor(x => x.Year).GreaterThan(2000);
            RuleFor(x => x.Month).InclusiveBetween(1, 12);
        }
    }


}
