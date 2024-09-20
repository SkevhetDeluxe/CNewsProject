namespace CNewsProject.Models.Api.Weather
{
	public class HistoricalWeather
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string? City { get; set; }
		public float Temperature { get; set; }
		public string? Condition { get; set; }
	}
}
