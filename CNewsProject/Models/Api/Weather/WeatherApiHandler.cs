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

            PosUrl = "https://www.openstreetmap.org/search?query=";
            // PosUrl = "https://www.google.se/maps/place/";





        }

        public async Task<WeatherStats> GetPositionAsync(string Place)
        {
            var client = new HttpClient();

            string url = PosUrl + Place + '/';

            var response = await client.GetAsync(url);



            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(json);

                WeatherStats gps = data.ToObject<WeatherStats>();

                return gps;
            }
            else
                return new WeatherStats();
        }

        public async Task<WeatherStats> GetPositionAsync() /// Default
        {
            var client = new HttpClient();

            string url = PosUrl + "Stockholm/";

            //string url4 = uri.AbsoluteUri;
            //string r3 = Request.Url.GetLeftPart(UriPartial.Authority)
            //string r1 = HttpContext.Current.Request.Url.AbsoluteUri;
            //string r2 = HttpContext.Current.Request.Url.AbsolutePath;

            var response = await client.GetAsync(url);
            var resp = await client.GetStringAsync (url);
            



            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(json);

                WeatherStats gps = data.ToObject<WeatherStats>();
                int i = 0;
                while (i< 100)
                {
                    i++;

                }


                return gps;
            }
            else
                return new WeatherStats();
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
