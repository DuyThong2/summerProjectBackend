using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Scheduling.API.Data.Configuration
{
    public class AdHocMealConfiguration : IEntityTypeConfiguration<AdHocMeal>
    {
        public void Configure(EntityTypeBuilder<AdHocMeal> builder)
        {
            builder.ToTable("AdHocMeals");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.TimeSlot)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(x => x.Meal)
                .WithMany()
                .HasForeignKey(x => x.MealId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.ScheduleCollectionId, x.Date });
        }
    }
}
