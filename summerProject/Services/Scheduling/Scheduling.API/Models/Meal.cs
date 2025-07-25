namespace Scheduling.API.Models
{
    public class Meal
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public IEnumerable<ScheduleTemplateDetail> TemplateDetails { get; set; }
        public IEnumerable<ScheduleInstanceDetail> InstanceDetails { get; set; }
    }

}
