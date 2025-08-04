namespace Scheduling.API.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ScheduleOverrideConfiguration : IEntityTypeConfiguration<ScheduleOverride>
    {
        public void Configure(EntityTypeBuilder<ScheduleOverride> builder)
        {
            builder.ToTable("ScheduleOverrides");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.TimeSlot)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.Action)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(x => x.TargetRule)
                .WithMany()
                .HasForeignKey(x => x.TargetRuleId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.ReplacementMeal)
                .WithMany()
                .HasForeignKey(x => x.ReplacementMealId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(x => new { x.ScheduleCollectionId, x.Date, x.TimeSlot });
        }
    }

}
