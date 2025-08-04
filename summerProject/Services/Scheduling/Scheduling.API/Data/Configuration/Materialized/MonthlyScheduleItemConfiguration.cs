using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.API.Models.Materialized;

namespace Scheduling.API.Data.Configuration.Materialized
{
    public class MonthlyScheduleItemConfiguration : IEntityTypeConfiguration<MonthlyScheduleItem>
    {
        public void Configure(EntityTypeBuilder<MonthlyScheduleItem> builder)
        {
            builder.ToTable("MonthlyScheduleItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date).IsRequired();

            builder.Property(x => x.TimeSlot)
                   .HasConversion<string>()   // enum → string cho dễ đọc
                   .IsRequired();

            builder.Property(x => x.Source)
                   .HasConversion<string>()   // enum → string
                   .IsRequired();

            // FK tới Meal
            builder.HasOne(x => x.Meal)
                   .WithMany()
                   .HasForeignKey(x => x.MealId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Index hay dùng để query
            builder.HasIndex(x => new { x.MonthlyScheduleInstanceId, x.Date });
            builder.HasIndex(x => new { x.Date, x.TimeSlot });
            builder.HasIndex(x => new { x.Source, x.SourceId });
        }
    }
}
