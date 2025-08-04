namespace Scheduling.API.Models
{
    public class ScheduleTemplateDetail
    {
        public Guid Id { get; set; }
        public Guid TemplateId { get; set; }
        public ScheduleTemplate ScheduleTemplate { get; set; } = default!;

        public DayOfWeek DayOfWeek { get; set; }
        public TimeSlot TimeSlot { get; set; }  // Enum: Breakfast, Lunch, ...
        public Guid MealId { get; set; }
        public Meal Meal { get; set; } = default!;
    }
}