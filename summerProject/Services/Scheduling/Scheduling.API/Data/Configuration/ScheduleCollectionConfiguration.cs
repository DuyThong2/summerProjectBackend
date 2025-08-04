using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Scheduling.API.Data.Configuration
{
    public class ScheduleCollectionConfiguration : IEntityTypeConfiguration<ScheduleCollection>
    {
        public void Configure(EntityTypeBuilder<ScheduleCollection> builder)
        {
            builder.ToTable("ScheduleCollections");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.StartYear).IsRequired();
            builder.Property(x => x.StartMonth).IsRequired();

            builder.HasIndex(x => new { x.UserId, x.StartYear, x.StartMonth });

            builder.HasOne(x => x.BaseTemplate)
                .WithMany()
                .HasForeignKey(x => x.BaseTemplateId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Rules)
                .WithOne(r => r.ScheduleCollection)
                .HasForeignKey(r => r.ScheduleCollectionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.AdHocMeals)
                .WithOne(a => a.ScheduleCollection)
                .HasForeignKey(a => a.ScheduleCollectionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Overrides)
                .WithOne(o => o.ScheduleCollection)
                .HasForeignKey(o => o.ScheduleCollectionId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
