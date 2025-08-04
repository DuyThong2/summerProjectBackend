using Microsoft.EntityFrameworkCore;
using Scheduling.API.Models;
using Scheduling.API.Models.Materialized;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Scheduling.API.Data
{
    public class SchedulingDbContext : DbContext, IScheduleDbContext
    {
        public SchedulingDbContext(DbContextOptions<SchedulingDbContext> options) : base(options) { }

        public DbSet<Meal> Meals => Set<Meal>();
        public DbSet<ScheduleTemplate> ScheduleTemplates => Set<ScheduleTemplate>();
        public DbSet<ScheduleTemplateDetail> ScheduleTemplateDetails => Set<ScheduleTemplateDetail>();
        public DbSet<ScheduleCollection> ScheduleCollections => Set<ScheduleCollection>();
        public DbSet<RecurringMealRule> RecurringMealRules => Set<RecurringMealRule>();
        public DbSet<AdHocMeal> AdHocMeals => Set<AdHocMeal>();
        public DbSet<ScheduleOverride> ScheduleOverrides => Set<ScheduleOverride>();

        public DbSet<MonthlyScheduleInstance> MonthlyScheduleInstances => Set<MonthlyScheduleInstance>();
        public DbSet<MonthlyScheduleItem> MonthlyScheduleItems => Set<MonthlyScheduleItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }

}
