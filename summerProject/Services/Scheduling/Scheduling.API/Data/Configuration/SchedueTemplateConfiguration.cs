using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Scheduling.API.Data.Configuration
{
    public class SchedueTemplateConfiguration : IEntityTypeConfiguration<ScheduleTemplate>
    {
        public void Configure(EntityTypeBuilder<ScheduleTemplate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(x => x.TemplateDetails)
                .WithOne(d => d.ScheduleTemplate)
                .HasForeignKey(d => d.ScheduleTemplateId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
