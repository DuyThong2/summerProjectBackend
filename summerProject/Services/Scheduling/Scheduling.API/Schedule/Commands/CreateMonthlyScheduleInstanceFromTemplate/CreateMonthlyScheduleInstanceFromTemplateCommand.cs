using FluentValidation;

namespace Scheduling.API.Schedule.Commands.CreateMonthlyScheduleInstanceFromTemplate
{
    public record CreateMonthlyScheduleInstanceFromTemplateCommand(
    Guid ScheduleCollectionId,
    int Year,
    int Month,
    Guid TemplateId,
    bool OverwriteIfExists = false // true = nếu đã tồn tại instance của tháng này thì xoá & tạo lại
) : ICommand<CreateMonthlyScheduleInstanceFromTemplateResult>;

    public record CreateMonthlyScheduleInstanceFromTemplateResult(Guid InstanceId, int ItemCount);

    public class CreateMonthlyScheduleInstanceFromTemplateValidator
    : AbstractValidator<CreateMonthlyScheduleInstanceFromTemplateCommand>
    {
        public CreateMonthlyScheduleInstanceFromTemplateValidator()
        {
            RuleFor(x => x.ScheduleCollectionId).NotEmpty();
            RuleFor(x => x.TemplateId).NotEmpty();
            RuleFor(x => x.Year).GreaterThan(2000);
            RuleFor(x => x.Month).InclusiveBetween(1, 12);
        }
    }

}
