using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.API.Models;

namespace Scheduling.API.Data.Configuration
{
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {


        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasMaxLength(255);

            builder.HasMany(x => x.TemplateDetails)
                .WithOne(d => d.Meal)
                .HasForeignKey(d => d.MealId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.InstanceDetails)
                .WithOne(d => d.Meal)
                .HasForeignKey(d => d.MealId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
