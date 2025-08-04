namespace Scheduling.Infrastructure.Data.Extensions
{
    internal static class InitialData
    {
        // GUID cố định cho các Meal
        public static readonly Guid OatmealId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        public static readonly Guid ChickenSaladId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        public static readonly Guid PastaId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        public static readonly Meal[] Meals =
        {
            new() { Id = OatmealId,      Name = "Oatmeal",        Description = "Breakfast meal" },
            new() { Id = ChickenSaladId, Name = "Chicken Salad",  Description = "Lunch meal" },
            new() { Id = PastaId,        Name = "Pasta",          Description = "Dinner meal" }
        };

        // GUID cố định cho Template
        public static readonly Guid WeeklyTemplateId = Guid.Parse("44444444-4444-4444-4444-444444444444");

        // GUID cố định cho từng ScheduleTemplateDetail (theo thứ trong tuần)
        public static readonly Dictionary<string, Guid> TemplateDetailIds = new()
        {
            { "Monday_Breakfast",  Guid.Parse("aaaaaaaa-0000-0000-0000-000000000001") },
            { "Monday_Lunch",      Guid.Parse("aaaaaaaa-0000-0000-0000-000000000002") },
            { "Monday_Dinner",     Guid.Parse("aaaaaaaa-0000-0000-0000-000000000003") },
            { "Tuesday_Breakfast", Guid.Parse("aaaaaaaa-0000-0000-0000-000000000004") },
            { "Tuesday_Lunch",     Guid.Parse("aaaaaaaa-0000-0000-0000-000000000005") },
            { "Tuesday_Dinner",    Guid.Parse("aaaaaaaa-0000-0000-0000-000000000006") },
            { "Wednesday_Breakfast", Guid.Parse("aaaaaaaa-0000-0000-0000-000000000007") },
            { "Wednesday_Lunch",     Guid.Parse("aaaaaaaa-0000-0000-0000-000000000008") },
            { "Wednesday_Dinner",    Guid.Parse("aaaaaaaa-0000-0000-0000-000000000009") },
            { "Thursday_Breakfast",  Guid.Parse("aaaaaaaa-0000-0000-0000-000000000010") },
            { "Thursday_Lunch",      Guid.Parse("aaaaaaaa-0000-0000-0000-000000000011") },
            { "Thursday_Dinner",     Guid.Parse("aaaaaaaa-0000-0000-0000-000000000012") },
            { "Friday_Breakfast",    Guid.Parse("aaaaaaaa-0000-0000-0000-000000000013") },
            { "Friday_Lunch",        Guid.Parse("aaaaaaaa-0000-0000-0000-000000000014") },
            { "Friday_Dinner",       Guid.Parse("aaaaaaaa-0000-0000-0000-000000000015") }
        };

        public static ScheduleTemplate BuildWeeklyTemplate(Guid breakfastMealId, Guid lunchMealId, Guid dinnerMealId)
        {
            var details = new List<ScheduleTemplateDetail>();

            foreach (DayOfWeek dow in Enum.GetValues(typeof(DayOfWeek)))
            {
                if (dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday) continue;

                details.Add(new ScheduleTemplateDetail
                {
                    Id = TemplateDetailIds[$"{dow}_Breakfast"],
                    TemplateId = WeeklyTemplateId,
                    DayOfWeek = dow,
                    TimeSlot = TimeSlot.Breakfast,
                    MealId = breakfastMealId
                });

                details.Add(new ScheduleTemplateDetail
                {
                    Id = TemplateDetailIds[$"{dow}_Lunch"],
                    TemplateId = WeeklyTemplateId,
                    DayOfWeek = dow,
                    TimeSlot = TimeSlot.Lunch,
                    MealId = lunchMealId
                });

                details.Add(new ScheduleTemplateDetail
                {
                    Id = TemplateDetailIds[$"{dow}_Dinner"],
                    TemplateId = WeeklyTemplateId,
                    DayOfWeek = dow,
                    TimeSlot = TimeSlot.Dinner,
                    MealId = dinnerMealId
                });
            }

            return new ScheduleTemplate
            {
                Id = WeeklyTemplateId,
                Name = "Weekly Default",
                TemplateDetails = details
            };
        }

        // GUID cố định cho Collection
        public static readonly Guid DemoCollectionId = Guid.Parse("55555555-5555-5555-5555-555555555555");

        public static ScheduleCollection BuildCollection(Guid baseTemplateId)
        {
            var now = DateTime.UtcNow;
            return new ScheduleCollection
            {
                Id = DemoCollectionId,
                UserId = "demo-user",
                StartYear = now.Year,
                StartMonth = now.Month,
                AutoRepeatMonthly = true,
                BaseTemplateId = baseTemplateId
            };
        }

        // GUID cố định cho Rule và AdHoc (ví dụ)
        public static readonly Guid WeeklyRuleId = Guid.Parse("66666666-6666-6666-6666-666666666666");
        public static readonly Guid AdHocId = Guid.Parse("77777777-7777-7777-7777-777777777777");

        public static RecurringMealRule BuildWeeklyRule(Guid collectionId, Guid mealId, TimeSlot timeSlot)
            => new()
            {
                Id = WeeklyRuleId,
                ScheduleCollectionId = collectionId,
                MealId = mealId,
                TimeSlot = timeSlot,
                Frequency = RecurrenceFrequency.Weekly,
                DaysOfWeek = DayOfWeekMask.Monday,
                Interval = 1,
                StartDate = DateTime.UtcNow.Date
            };

        public static AdHocMeal BuildAdHoc(Guid collectionId, DateTime date, TimeSlot timeSlot, Guid mealId)
            => new()
            {
                Id = AdHocId,
                ScheduleCollectionId = collectionId,
                Date = date,
                TimeSlot = timeSlot,
                MealId = mealId
            };
    }
}
