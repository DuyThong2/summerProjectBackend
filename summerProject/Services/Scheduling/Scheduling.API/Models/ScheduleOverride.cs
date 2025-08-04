namespace Scheduling.API.Models
{
    public class ScheduleOverride
    {
        public Guid Id { get; set; }
        public Guid ScheduleCollectionId { get; set; }
        public ScheduleCollection ScheduleCollection { get; set; } = default!;

        public DateTime Date { get; set; }
        public TimeSlot TimeSlot { get; set; }

        // target để xác định override cái gì (rule nào / meal nào)
        public Guid? TargetRuleId { get; set; }
        public RecurringMealRule? TargetRule { get; set; }

        public OverrideAction Action { get; set; }

        public Guid? ReplacementMealId { get; set; }
        public Meal? ReplacementMeal { get; set; }
    }
}