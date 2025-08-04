namespace Scheduling.API.Data.Configuration.Materialized
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Scheduling.API.Models.Materialized;

    public class MonthlyScheduleInstanceConfiguration : IEntityTypeConfiguration<MonthlyScheduleInstance>
    {
        public void Configure(EntityTypeBuilder<MonthlyScheduleInstance> builder)
        {
            builder.ToTable("MonthlyScheduleInstances");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Year).IsRequired();
            builder.Property(x => x.Month).IsRequired();
            builder.Property(x => x.GeneratedAt).IsRequired();

            // Mỗi collection chỉ có tối đa 1 instance cho (Year, Month)
            builder.HasIndex(x => new { x.ScheduleCollectionId, x.Year, x.Month })
                   .IsUnique();

            builder.HasOne(x => x.ScheduleCollection)
                   .WithMany() // hoặc WithMany(c => c.MonthlyInstances) nếu bạn có navigation
                   .HasForeignKey(x => x.ScheduleCollectionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.AppliedTemplate)
                   .WithMany()
                   .HasForeignKey(x => x.AppliedTemplateId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.Items)
                   .WithOne(i => i.MonthlyScheduleInstance)
                   .HasForeignKey(i => i.MonthlyScheduleInstanceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
