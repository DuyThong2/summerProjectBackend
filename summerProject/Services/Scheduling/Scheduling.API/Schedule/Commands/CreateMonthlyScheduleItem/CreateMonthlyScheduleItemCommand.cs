using FluentValidation;
using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Commands.NewFolder
{
    public record CreateMonthlyScheduleItemCommand(
        Guid MonthlyInstanceId,
        MonthlyScheduleItemDto Item
    ) : ICommand<CreateMonthlyScheduleItemResult>;

    public record CreateMonthlyScheduleItemResult(Guid Id);

    public class CreateMonthlyScheduleItemValidator : AbstractValidator<CreateMonthlyScheduleItemCommand>
    {
        public CreateMonthlyScheduleItemValidator()
        {
            RuleFor(x => x.MonthlyInstanceId).NotEmpty();
            RuleFor(x => x.Item.MealId).NotEmpty();
            RuleFor(x => x.Item.Date).NotEmpty();
        }
    }


}
