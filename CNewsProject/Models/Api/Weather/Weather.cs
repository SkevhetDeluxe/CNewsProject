using System.Text.Json.Serialization;

namespace CNewsProject.Models.Api.Weather
{
    //Old Api
    #region Old_API
    //public class Hourly
    //{
    //    public List<string> ?Time { get; set; }
    //    public List<double> ?Temperature_2m { get; set; }
    //}

    //public class HourlyUnits
    //{
    //    public string ?Time { get; set; }
    //    public string ?Temperature_2m { get; set; }
    //}

    //public class WeatherStats
    //{
    //    public double Latitude { get; set; }
    //    public double Longitude { get; set; }
    //    public double Generationtime_ms { get; set; }
    //    public int Utc_offset_seconds { get; set; }
    //    public string ?Timezone { get; set; }
    //    public string ?Timezone_abbreviation { get; set; }
    //    public int Elevation { get; set; }
    //    public HourlyUnits ?Hourly_units { get; set; }
    //    public Hourly ?Hourly { get; set; }

    //    public string ?CurrentTime { get; set; }
    //    public double CurrentTemperature { get; set; }
    //}
    #endregion

    #region SMHI API

    public class TestingPurpose
    {
        public WeatherStats EmptyStats { get; set; } = new();
        public TestingPurpose()
        {
            
        }

        public void test()
        {
            //TempStat newTemp = EmptyStats.timeSeries[0].TempStats[0];
            //2024-08-07T11:00:00Z
            //{2024-08-07 17:00:00}

            DateTime testTime = new();
            testTime = Convert.ToDateTime("2024-08-07 17:00:00");

            var test = EmptyStats.timeSeries.FirstOrDefault(ts => ts.validTime == testTime).parameters.FirstOrDefault(p => p.name == "t");

            //var test = EmptyStats.timeSeries[0].TempStats.FirstOrDefault(s => s.name == "t");
        }
    }

    public class WeatherStats
    {
        public DateTime approvedTime { get; set; }
        public DateTime referenceTime { get; set; }
        public Geometry geometry { get; set; }
        public List<Timesery> timeSeries { get; set; }
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
}
