using Microsoft.EntityFrameworkCore;
using Scheduling.API.Models;
using System.Collections.Generic;

namespace Scheduling.API.Data
{
    public interface IScheduleDbContext
    {
        DbSet<Meal> Meals { get; }
        DbSet<ScheduleTemplate> ScheduleTemplates { get; }
        DbSet<ScheduleTemplateDetail> ScheduleTemplateDetails { get; }
        DbSet<ScheduleInstance> ScheduleInstances { get; }
        DbSet<ScheduleInstanceDetail> ScheduleInstanceDetails { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
