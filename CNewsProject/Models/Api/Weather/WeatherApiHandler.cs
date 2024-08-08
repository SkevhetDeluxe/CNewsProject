using CNewsProject.Models.DataBase;
using Newtonsoft.Json.Linq;
using System.Drawing.Drawing2D;
using System.Drawing;
using System;
using System.Text.Json.Nodes;

namespace CNewsProject.Models.Api.Weather
{
    public class WeatherApiHandler : IWeatherApiHandler
    {

        // FOR REFERENCE
        // https:// api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m
        // https:// api/category/pmp3g/version/2/geotype/point/lon/16/lat/58/data.json

        public string BaseUrl { get; set; } 
        public string EndUrl { get; set; } 
        public string Latitude { get; set; } 
        public string Longitude { get; set; }
        


        public WeatherApiHandler()
        {
            //BaseUrl = "https:// api.open-meteo.com/v1/forecast?";
            // EndUrl = "&hourly=temperature_2m";
            //Latitude = "latitude=";
            //Longitude = "&longitude=";

            //For Reference https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/16/lat/58/data.json

            BaseUrl = "https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/";
            EndUrl = "/data.json";
            Longitude = "lon/";
            Latitude = "/lat/";
            
        }

        public async Task<WeatherStats> GetWeatherAsync(string latVal, string longVal)
        {
            var client = new HttpClient();

            string url = BaseUrl + Latitude + latVal + Longitude + longVal + EndUrl;

            var response = await client.GetAsync(url);

            

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(json);

                WeatherStats stats = data.ToObject<WeatherStats>();

                return stats;
            }
            else
                return new WeatherStats();
        }

        public async Task<WeatherStats> GetWeatherAsync() // Default overload MOTHERFUCKER!
        {
            var client = new HttpClient();

            string latVal = "58.25"; string longVal = "15.35";

            string url = BaseUrl + Longitude + longVal + Latitude + latVal + EndUrl;

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(json);

                WeatherStats stats = data.ToObject<WeatherStats>();

                return stats;
            }
            else
                return new WeatherStats();
        }
    }
}
