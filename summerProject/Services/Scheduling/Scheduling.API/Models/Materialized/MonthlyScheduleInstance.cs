namespace Scheduling.API.Models.Materialized
{
    public class MonthlyScheduleInstance
    {
        public Guid Id { get; set; }
        public Guid ScheduleCollectionId { get; set; }
        public ScheduleCollection ScheduleCollection { get; set; } = default!;

        public int Year { get; set; }
        public int Month { get; set; }

        public DateTime GeneratedAt { get; set; }

        public Guid? AppliedTemplateId { get; set; }
        public ScheduleTemplate? AppliedTemplate { get; set; }

        public ICollection<MonthlyScheduleItem> Items { get; set; } = new List<MonthlyScheduleItem>();
    }

    

    

}
