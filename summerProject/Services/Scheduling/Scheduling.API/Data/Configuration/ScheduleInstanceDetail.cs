using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Scheduling.API.Data.Configuration
{
    public class ScheduleInstanceDetailConfiguration : IEntityTypeConfiguration<ScheduleInstanceDetail>
    {
        public void Configure(EntityTypeBuilder<ScheduleInstanceDetail> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ScheduledDate)
                .IsRequired();

            builder.Property(x => x.TimeSlot)
                .HasConversion<string>() // Lưu TimeSlot dưới dạng string
                .IsRequired();

            builder.HasOne(x => x.Meal)
                .WithMany(m => m.InstanceDetails)
                .HasForeignKey(x => x.MealId);

            builder.HasOne(x => x.ScheduleInstance)
                .WithMany(i => i.InstanceDetails)
                .HasForeignKey(x => x.ScheduleInstanceId);
        }
    }
}
