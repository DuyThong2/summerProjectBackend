using FluentValidation;
using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Commands.UpdateMonthlyScheduleItem
{
    public record UpdateMonthlyScheduleItemCommand(
    Guid Id,
    MonthlyScheduleItemDto Item
) : ICommand<UpdateMonthlyScheduleItemResult>;

    public record UpdateMonthlyScheduleItemResult(bool IsSuccess);


    public class UpdateMonthlyScheduleItemValidator : AbstractValidator<UpdateMonthlyScheduleItemCommand>
    {
        public UpdateMonthlyScheduleItemValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Item.MealId).NotEmpty();
            RuleFor(x => x.Item.Date).NotEmpty();
        }
    }

}
