namespace Scheduling.API.Models
{
    public class ScheduleTemplate
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public IEnumerable<ScheduleTemplateDetail> TemplateDetails { get; set; }
    }

}
