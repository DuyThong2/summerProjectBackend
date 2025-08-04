using Microsoft.EntityFrameworkCore;
using Scheduling.API.Models;
using Scheduling.API.Models.Materialized;
using System.Collections.Generic;

namespace Scheduling.API.Data
{
    public interface IScheduleDbContext
    {
        public DbSet<Meal> Meals { get; }
        public DbSet<ScheduleTemplate> ScheduleTemplates { get; }
        public DbSet<ScheduleTemplateDetail> ScheduleTemplateDetails { get; }
        public DbSet<ScheduleCollection> ScheduleCollections { get; }
        public DbSet<RecurringMealRule> RecurringMealRules { get; }
        public DbSet<AdHocMeal> AdHocMeals { get; }
        public DbSet<ScheduleOverride> ScheduleOverrides { get; }

        public DbSet<MonthlyScheduleInstance> MonthlyScheduleInstances { get; }
        public DbSet<MonthlyScheduleItem> MonthlyScheduleItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
