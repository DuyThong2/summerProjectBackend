using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.API.Models;

namespace Scheduling.API.Data.Configuration
{
    public class ScheduleInstanceConfiguration : IEntityTypeConfiguration<ScheduleInstance>
    {


        public void Configure(EntityTypeBuilder<ScheduleInstance> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .HasMaxLength(100);

            builder.Property(x => x.StartDate)
                .IsRequired();

            builder.Property(x => x.EndDate)
                .IsRequired();

            builder.HasOne(x => x.AppliedTemplate)
                .WithMany()
                .HasForeignKey(x => x.AppliedTemplateId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.InstanceDetails)
                .WithOne(d => d.ScheduleInstance)
                .HasForeignKey(d => d.ScheduleInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
