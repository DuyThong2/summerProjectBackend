namespace Scheduling.API.Models
{
    public class ScheduleTemplate
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;

        // Thông tin mô tả template
        public string? Description { get; set; }


       
        public ICollection<ScheduleTemplateDetail> TemplateDetails { get; set; } = new List<ScheduleTemplateDetail>();
    }

}