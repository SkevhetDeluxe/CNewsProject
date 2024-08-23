using System.Text.Json.Serialization;

namespace CNewsProject.Models.Api.Weather
{
	//Old Api
	#region Old_API
	public class WeatherStats
	{
		public float latitude { get; set; }
		public float longitude { get; set; }
		public float generationtime_ms { get; set; }
		public int utc_offset_seconds { get; set; }
		public string timezone { get; set; }
		public string timezone_abbreviation { get; set; }
		public int elevation { get; set; }
		public Current_Units current_units { get; set; }
		public Current current { get; set; }
		public Hourly_Units hourly_units { get; set; }
		public Hourly hourly { get; set; }

		public string NameOfCity { get; set; } = "Stockholm";
		public bool IsPlaceholder { get; set; }
	}

	public class Current_Units
	{
		public string time { get; set; }
		public string interval { get; set; }
		public string temperature_2m { get; set; }
	}

	public class Current
	{
		public string time { get; set; }
		public int interval { get; set; }
		public int temperature_2m { get; set; }
	}

	public class Hourly_Units
	{
		public string time { get; set; }
		public string temperature_2m { get; set; }
		public string weather_code { get; set; }
	}

	public class Hourly
	{
		public string[] time { get; set; }
		public float[] temperature_2m { get; set; }
		public int[] weather_code { get; set; }
	}


}
#endregion

#region SMHI API

//public class TestingPurpose
//{
//    public WeatherStats EmptyStats { get; set; } = new();
//    public TestingPurpose()
//    {

//    }

//    public void test()
//    {
//        //TempStat newTemp = EmptyStats.timeSeries[0].TempStats[0];
//        //2024-08-07T11:00:00Z
//        //{2024-08-07 17:00:00}

//        DateTime testTime = new();
//        testTime = Convert.ToDateTime("2024-08-07 17:00:00");

//        var test = EmptyStats.timeSeries.FirstOrDefault(ts => ts.validTime == testTime).parameters.FirstOrDefault(p => p.name == "t");

//        //var test = EmptyStats.timeSeries[0].TempStats.FirstOrDefault(s => s.name == "t");
//    }
//}

public class WeatherStatsSMHI
{
	public DateTime approvedTime { get; set; }
	public DateTime referenceTime { get; set; }
	public Geometry geometry { get; set; }
	public List<Timesery> timeSeries { get; set; }
	public string NameOfCity { get; set; } = "Stockholm";
}

public class Geometry
{
	public string type { get; set; }
	public float[][] coordinates { get; set; }
}

public class Timesery
{
	public DateTime validTime { get; set; }
	public List<Parameter> parameters { get; set; }
}

public class Parameter
{
	public string name { get; set; }
	public string levelType { get; set; }
	public int level { get; set; }
	public string unit { get; set; }
	public float[] values { get; set; }
}


#endregion

