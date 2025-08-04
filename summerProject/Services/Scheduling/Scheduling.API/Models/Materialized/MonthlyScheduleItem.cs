using Scheduling.API.Enums.Materialized;

namespace Scheduling.API.Models.Materialized
{
    public class MonthlyScheduleItem
    {
        public Guid Id { get; set; }
        public Guid MonthlyScheduleInstanceId { get; set; }
        public MonthlyScheduleInstance MonthlyScheduleInstance { get; set; } = default!;

        public DateTime Date { get; set; }
        public TimeSlot TimeSlot { get; set; }

        public Guid MealId { get; set; }
        public Meal Meal { get; set; } = default!;

        public ScheduleItemSource Source { get; set; }
        public Guid? SourceId { get; set; } // RuleId / AdHocId / OverrideId / TemplateDetailId
    }
}
