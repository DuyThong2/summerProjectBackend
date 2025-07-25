namespace Scheduling.API.Models
{
    public class ScheduleInstance
    {
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? UserId { get; set; } // nếu gắn cho user

        public Guid? AppliedTemplateId { get; set; }
        public ScheduleTemplate? AppliedTemplate { get; set; }

        public IEnumerable<ScheduleInstanceDetail> InstanceDetails { get; set; }
    }

}
