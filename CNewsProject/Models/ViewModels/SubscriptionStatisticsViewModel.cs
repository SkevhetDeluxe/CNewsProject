namespace CNewsProject.Models.ViewModels
{
	public class SubscriptionStatisticsViewModel
	{
		public int TotalSubscribers { get; set; }
		public int NewSubscribersLastWeek { get; set; }
		public List<DailySubscriptionStats> DailySubscriptions { get; set; }
	}
}
