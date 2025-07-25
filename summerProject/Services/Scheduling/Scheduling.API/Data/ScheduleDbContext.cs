using Microsoft.EntityFrameworkCore;
using Scheduling.API.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Scheduling.API.Data
{
    public class SchedulingDbContext : DbContext, IScheduleDbContext
    {
        public SchedulingDbContext(DbContextOptions<SchedulingDbContext> options) : base(options) { }

        public DbSet<Meal> Meals => Set<Meal>();

        public DbSet<ScheduleTemplate> ScheduleTemplates => Set<ScheduleTemplate>();

        public DbSet<ScheduleTemplateDetail> ScheduleTemplateDetails => Set<ScheduleTemplateDetail>();

        public DbSet<ScheduleInstance> ScheduleInstances => Set<ScheduleInstance>();

        public DbSet<ScheduleInstanceDetail> ScheduleInstanceDetails => Set<ScheduleInstanceDetail>();

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchedulingDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }

}
