using Scheduling.API.Enums;

namespace Scheduling.API.Models
{
    public class ScheduleInstanceDetail
    {
        public Guid Id { get; set; }

        public Guid ScheduleInstanceId { get; set; }
        public ScheduleInstance ScheduleInstance { get; set; }

        public Guid MealId { get; set; }
        public Meal Meal { get; set; }

        public DateTime ScheduledDate { get; set; }
        public TimeSlot TimeSlot { get; set; } 
    }

}
