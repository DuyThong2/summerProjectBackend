using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scheduling.API.Data;
using Scheduling.API.Enums;
using Scheduling.API.Enums.Materialized;
using Scheduling.API.Models;
using Scheduling.API.Models.Materialized;

namespace Scheduling.Infrastructure.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<SchedulingDbContext>();

            await context.Database.MigrateAsync();

            await SeedAsync(context);
        }

        private static async Task SeedAsync(SchedulingDbContext context)
        {
            await SeedMealsAsync(context);
            await SeedTemplatesAsync(context);
            await SeedCollectionsRulesAndAdHocAsync(context);
            await SeedMonthlyScheduleAsync(context);
        }

        private static async Task SeedMealsAsync(SchedulingDbContext context)
        {
            if (await context.Meals.AnyAsync()) return;

            await context.Meals.AddRangeAsync(InitialData.Meals);
            await context.SaveChangesAsync();
        }

        private static async Task SeedTemplatesAsync(SchedulingDbContext context)
        {
            if (await context.ScheduleTemplates.AnyAsync()) return;

            // attach meals (already seeded) to template details
            var breakfast = await context.Meals.FirstAsync(m => m.Name == "Oatmeal");
            var lunch = await context.Meals.FirstAsync(m => m.Name == "Chicken Salad");
            var dinner = await context.Meals.FirstAsync(m => m.Name == "Pasta");

            var template = InitialData.BuildWeeklyTemplate(breakfast.Id, lunch.Id, dinner.Id);

            await context.ScheduleTemplates.AddAsync(template);
            await context.SaveChangesAsync();
        }

        private static async Task SeedCollectionsRulesAndAdHocAsync(SchedulingDbContext context)
        {
            if (await context.ScheduleCollections.AnyAsync()) return;

            var breakfast = await context.Meals.FirstAsync(m => m.Name == "Oatmeal");

            var template = await context.ScheduleTemplates.FirstAsync();

            var collection = InitialData.BuildCollection(template.Id);
            await context.ScheduleCollections.AddAsync(collection);
            await context.SaveChangesAsync();

            // Recurring rule: Every Monday Breakfast Oatmeal
            var rule = InitialData.BuildWeeklyRule(collection.Id, breakfast.Id, TimeSlot.Breakfast);
            await context.RecurringMealRules.AddAsync(rule);

            // AdHoc: hôm nay ăn thêm Pasta buổi tối
            var pasta = await context.Meals.FirstAsync(m => m.Name == "Pasta");
            var adHoc = InitialData.BuildAdHoc(collection.Id, DateTime.UtcNow.Date, TimeSlot.Dinner, pasta.Id);
            await context.AdHocMeals.AddAsync(adHoc);

            await context.SaveChangesAsync();
        }

        private static async Task SeedMonthlyScheduleAsync(SchedulingDbContext context)
        {
            // Nếu đã có instance cho tháng hiện tại thì bỏ qua
            var now = DateTime.UtcNow;
            var year = now.Year;
            var month = now.Month;

            var collection = await context.ScheduleCollections.FirstOrDefaultAsync();
            if (collection == null) return;

            var existed = await context.MonthlyScheduleInstances
                .AnyAsync(x => x.ScheduleCollectionId == collection.Id && x.Year == year && x.Month == month);

            if (existed) return;

            var template = await context.ScheduleTemplates
                .Include(t => t.TemplateDetails)
                .FirstOrDefaultAsync(t => t.Id == collection.BaseTemplateId);

            var instance = new MonthlyScheduleInstance
            {
                Id = Guid.NewGuid(),
                ScheduleCollectionId = collection.Id,
                Year = year,
                Month = month,
                AppliedTemplateId = template?.Id,
                GeneratedAt = DateTime.UtcNow
            };

            await context.MonthlyScheduleInstances.AddAsync(instance);
            await context.SaveChangesAsync();

            var items = new List<MonthlyScheduleItem>();

            // Build từ template (nếu có)
            if (template != null)
            {
                foreach (var day in DaysInMonth(year, month))
                {
                    var matched = template.TemplateDetails.Where(d => d.DayOfWeek == day.DayOfWeek);
                    foreach (var d in matched)
                    {
                        items.Add(new MonthlyScheduleItem
                        {
                            Id = Guid.NewGuid(),
                            MonthlyScheduleInstanceId = instance.Id,
                            Date = day,
                            TimeSlot = d.TimeSlot,
                            MealId = d.MealId,
                            Source = ScheduleItemSource.Template,
                            SourceId = d.Id
                        });
                    }
                }
            }

            // Bạn có thể thêm merge từ Rules + AdHoc ở đây nếu muốn.
            // Ở seed demo, mình chỉ add thêm 1 ad-hoc của collection (nếu thuộc tháng hiện tại).
            var adhocs = await context.AdHocMeals
                .Where(a => a.ScheduleCollectionId == collection.Id &&
                            a.Date.Year == year && a.Date.Month == month)
                .ToListAsync();

            foreach (var a in adhocs)
            {
                items.Add(new MonthlyScheduleItem
                {
                    Id = Guid.NewGuid(),
                    MonthlyScheduleInstanceId = instance.Id,
                    Date = a.Date,
                    TimeSlot = a.TimeSlot,
                    MealId = a.MealId,
                    Source = ScheduleItemSource.AdHoc,
                    SourceId = a.Id
                });
            }

            if (items.Any())
            {
                await context.MonthlyScheduleItems.AddRangeAsync(items);
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<DateTime> DaysInMonth(int year, int month)
        {
            var start = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
            var end = start.AddMonths(1).AddDays(-1);
            for (var d = start; d <= end; d = d.AddDays(1))
                yield return d;
        }
    }
}
