namespace Scheduling.API.Repository.Impl
{
    public class MealRepository : GenericRepository<Meal>, IMealRepository
    {
        public MealRepository(DbContext context) : base(context) { }

        public async Task<IEnumerable<Meal>> GetMealsWithDetailsAsync()
        {
            return await _context.Set<Meal>()
                .Include(m => m.TemplateDetails)
                .Include(m => m.InstanceDetails)
                .ToListAsync();
        }
    }
}
