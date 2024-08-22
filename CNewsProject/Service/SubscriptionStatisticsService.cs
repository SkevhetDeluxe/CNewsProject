namespace CNewsProject.Service
{
	public class SubscriptionStatisticsService: ISubscriptionStatisticsService
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
            var startDateOnly = DateOnly.FromDateTime(startDate);
            var endDateOnly = DateOnly.FromDateTime(endDate);

            return _dbContext.Subscription.Count(s => s.CreateDate >= startDateOnly && s.ExpiresDate <= endDateOnly);
        }

		public List<DailySubscriptionStats> GetDailySubscriptions(DateTime startDate, DateTime endDate)
		{
            return _dbContext.Subscription
         .Where(s => s.CreateDate.ToDateTime(new TimeOnly(0, 0)) >= startDate
                     && s.ExpiresDate.ToDateTime(new TimeOnly(23, 59)) <= endDate)
         .GroupBy(s => s.ExpiresDate.ToDateTime(new TimeOnly(0, 0)))
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
