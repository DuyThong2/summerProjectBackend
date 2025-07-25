using Scheduling.API.Enums;

namespace Scheduling.API.Models
{
    public class ScheduleTemplateDetail
    {
        public Guid Id { get; set; }

        public Guid ScheduleTemplateId { get; set; }
        public ScheduleTemplate ScheduleTemplate { get; set; }

        public Guid MealId { get; set; }
        public Meal Meal { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public TimeSlot TimeSlot { get; set; }
    }

}
