namespace Scheduling.API.Models
{
    public class AdHocMeal
    {
        public Guid Id { get; set; }
        public Guid ScheduleCollectionId { get; set; }
        public ScheduleCollection ScheduleCollection { get; set; } = default!;

        public DateTime Date { get; set; }       // ngày cụ thể
        public TimeSlot TimeSlot { get; set; }   // khung giờ cụ thể
        public Guid MealId { get; set; }
        public Meal Meal { get; set; } = default!;
    }

}