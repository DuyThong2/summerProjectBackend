using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Scheduling.API.Data.Configuration
{
    public class RecurringMealRuleConfiguration : IEntityTypeConfiguration<RecurringMealRule>
    {
        public void Configure(EntityTypeBuilder<RecurringMealRule> builder)
        {
            builder.ToTable("RecurringMealRules");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TimeSlot)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.Frequency)
                .HasConversion<string>()
                .IsRequired();

            // Flags enum -> int
            builder.Property(x => x.DaysOfWeek)
                .HasConversion<int?>();

            builder.Property(x => x.Interval)
                .HasDefaultValue(1);

            builder.Property(x => x.StartDate).IsRequired();

            builder.HasOne(x => x.Meal)
                .WithMany()
                .HasForeignKey(x => x.MealId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
