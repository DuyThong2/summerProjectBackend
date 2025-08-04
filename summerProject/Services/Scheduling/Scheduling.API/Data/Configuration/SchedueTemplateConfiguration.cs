using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Scheduling.API.Data.Configuration
{
    public class ScheduleTemplateConfiguration : IEntityTypeConfiguration<ScheduleTemplate>
    {
        public void Configure(EntityTypeBuilder<ScheduleTemplate> builder)
        {
            builder.ToTable("ScheduleTemplates");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.HasMany(x => x.TemplateDetails)
                .WithOne(d => d.ScheduleTemplate)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
