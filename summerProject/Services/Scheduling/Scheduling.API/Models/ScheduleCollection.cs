namespace Scheduling.API.Models
{
    public class ScheduleCollection
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;

        public int StartYear { get; set; }
        public int StartMonth { get; set; }
        public bool AutoRepeatMonthly { get; set; } = true;

        public Guid? BaseTemplateId { get; set; }
        public ScheduleTemplate? BaseTemplate { get; set; }

        public ICollection<RecurringMealRule> Rules { get; set; } = new List<RecurringMealRule>();
        public ICollection<AdHocMeal> AdHocMeals { get; set; } = new List<AdHocMeal>();
        public ICollection<ScheduleOverride> Overrides { get; set; } = new List<ScheduleOverride>();
    }


}
