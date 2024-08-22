namespace CNewsProject.Service
{
	public interface ISubscriptionStatisticsService
	{
		int GetTotalSubscribers();
		int GetNewSubscribers(DateTime startDate, DateTime endDate);
		List<DailySubscriptionStats> GetDailySubscriptions(DateTime startDate, DateTime endDate);
	}
}
