using CNewsProject.Models.DataBase;
using Newtonsoft.Json.Linq;
using System.Drawing.Drawing2D;
using System.Drawing;
using System;
using System.Text.Json.Nodes;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

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
        public string PosUrl { get; set; }
        public string Place { get; set; }




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

            PosUrl = "https://weatherapi.dreammaker-it.se/GeoLocation?query=";       

        }

        public async Task<GeoLocation> GetPositionAsync(string Place)
        {
            var client = new HttpClient();
            //string cor;
            string url = PosUrl + Place;

            var response = await client.GetAsync(url);
            var resp = await client.GetStringAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(json);

                GeoLocation gps = data.ToObject<GeoLocation>();
                //string[] latlog = json;
                //int i = 0, j = 0;
                //for (i = 0; i <= latlog.Length; i++)
                //    if (latlog[i] == ("1 to 9"))
                //        j = i;
                //for (j = i; j <= latlog.Length; j++)
                //       cor = cor + latlog[j];







                return null;
            }
            else
                return new GeoLocation();
        }

        public async Task<GeoLocation> GetPositionAsync() /// Default
        {
            var client = new HttpClient();

            string url = PosUrl + "Stockholm";
           
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(json);

                GeoLocation gps = data.ToObject<GeoLocation>();



                return gps;
            }
            else
                return new GeoLocation();
        }

        public async Task<WeatherStats> GetWeatherAsync(string longVal, string latVal)
        {
            var client = new HttpClient();

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

        public async Task<WeatherStats> GetWeatherAsync() // Default overload MOTHERFUCKER!
        {
            var client = new HttpClient();

            string latVal = "59.32"; string longVal = "17.98";

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
