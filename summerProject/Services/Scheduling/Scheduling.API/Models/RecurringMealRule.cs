using Scheduling.API.Enums;

namespace Scheduling.API.Models
{
    public class RecurringMealRule
    {
        public Guid Id { get; set; }
        public Guid ScheduleCollectionId { get; set; }
        public ScheduleCollection ScheduleCollection { get; set; } = default!;

        public Guid MealId { get; set; }
        public Meal Meal { get; set; } = default!;

        public TimeSlot TimeSlot { get; set; }

        public RecurrenceFrequency Frequency { get; set; } = RecurrenceFrequency.Weekly;

        // Weekly:
        public DayOfWeekMask? DaysOfWeek { get; set; } // ví dụ: Monday | Thursday

        // Monthly:
        public int? DayOfMonth { get; set; } // nếu muốn lặp ngày 5 hàng tháng

        public int Interval { get; set; } = 1; // mỗi 1 tuần / 1 tháng (có thể 2 tuần/lần ...)

        public DateTime StartDate { get; set; } // bắt đầu áp dụng rule
        public DateTime? EndDate { get; set; }  // null = vô hạn

        // Bạn có thể thêm trường Priority để merge rule / override
    }
}