namespace CNewsProject.Service
{
    public class SubscriptionStatisticsService : ISubscriptionStatisticsService
    {
        private readonly ApplicationDbContext _dbContext;

        public SubscriptionStatisticsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int GetTotalSubscribers()
        {
            return _dbContext.Subscription.Count();
        }

        public int GetNewSubscribers(DateTime startDate, DateTime endDate)
        {
            return _dbContext.Subscription.Count(s => s.RenewedDate >= startDate && s.ExpiresDate <= endDate);
        }

        public List<DailySubscriptionStats> GetDailySubscriptions(DateTime startDate, DateTime endDate)
        {
            return _dbContext.Subscription
            .Where(s => s.RenewedDate >= startDate && s.ExpiresDate <= endDate)
            .GroupBy(s => s.RenewedDate.Date)
            .Select(g => new DailySubscriptionStats
            {
                Date = g.Key,
                Count = g.Count()
            })
            .OrderBy(stats => stats.Date)
            .ToList();
        }
    }
}
