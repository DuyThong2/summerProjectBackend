using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.API.Models;

namespace Scheduling.API.Data.Configuration
{
    public class ScheduleTemplateDetailConfiguration : IEntityTypeConfiguration<ScheduleTemplateDetail>
    {
        public void Configure(EntityTypeBuilder<ScheduleTemplateDetail> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DayOfWeek)
                .IsRequired();

            builder.Property(x => x.TimeSlot)
                .HasConversion<string>()  // Lưu TimeSlot dưới dạng string
                .IsRequired();

            builder.HasOne(x => x.Meal)
                .WithMany(m => m.TemplateDetails)
                .HasForeignKey(x => x.MealId);

            builder.HasOne(x => x.ScheduleTemplate)
                .WithMany(t => t.TemplateDetails)
                .HasForeignKey(x => x.ScheduleTemplateId);
        }
    }

}
