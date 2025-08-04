namespace Scheduling.API.Models
{
    public class Meal
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public ICollection<ScheduleTemplateDetail> TemplateDetails { get; set; } = new List<ScheduleTemplateDetail>();
    }
}
