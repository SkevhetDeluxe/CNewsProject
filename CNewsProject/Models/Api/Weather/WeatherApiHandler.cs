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
		//https:// api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m
		// https:// api/category/pmp3g/version/2/geotype/point/lon/16/lat/58/data.json

		public string BaseUrl { get; set; }
		public string EndUrl { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
		public string PosUrl { get; set; }
		public string Place { get; set; }




		public WeatherApiHandler()
		{

			//For Reference https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/16/lat/58/data.json

			//BaseUrl = "https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/";
			//EndUrl = "/data.json";
			//Longitude = "lon/";
			//Latitude = "/lat/";

			//PosUrl = "https://weatherapi.dreammaker-it.se/GeoLocation?query=";

			// For Referens https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m
			// For Referens2 https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,weather_code&timezone=Europe%2FBerlin

			BaseUrl = "https://api.open-meteo.com/v1/forecast?";
			EndUrl = "&current=temperature_2m&hourly=temperature_2m,weather_code&timezone=Europe%2FBerlin";
			Longitude = "&longitude=";
			Latitude = "latitude=";

			PosUrl = "https://weatherapi.dreammaker-it.se/GeoLocation?query=";

		}

		public async Task<GeoLocation> GetPositionAsync(string Place)
		{
			var client = new HttpClient();
			string url = PosUrl + Place;

			var response = await client.GetAsync(url);
			var resp = await client.GetStringAsync(url);
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

		

		public string GetConditionFromInt(int condition)
		{
			string kindOfWeather;

			//Code	 Description

			switch (condition)
			{
				case 0:
					kindOfWeather = "Clear sky";
					break;
				case 1:
					kindOfWeather = "Mainly clear";
					break;
				case 2:
					kindOfWeather = "Partly cloudy";
					break;
				case 3:
					kindOfWeather = "Overcast"; ;
					break;
				case 45:
					kindOfWeather = "Fog";
					break;
				case 48:
					kindOfWeather = "Depositing rime fog";
					break;
				case 51:
					kindOfWeather = "Drizzle: Light";
					break;
				case 53:
					kindOfWeather = "Drizzle: Moderate";
					break;
				case 55:
					kindOfWeather = "Drizzle: Dense intensity";
					break;
				case 56:
					kindOfWeather = "Freezing Drizzle: Light";
					break;
				case 57:
					kindOfWeather = "Freezing Drizzle: Dense intensity";
					break;
				case 61:
					kindOfWeather = "Rain: Slight";
					break;
				case 63:
					kindOfWeather = "Rain: Moderate";
					break;
				case 65:
					kindOfWeather = "Rain: Heavy intensity";
					break;
				case 66:
					kindOfWeather = "Freezing Rain: Light";
					break;
				case 67:
					kindOfWeather = "Freezing Rain: Heavy intensity";
					break;
				case 71:
					kindOfWeather = "Snow fall: Slight";
					break;
				case 73:
					kindOfWeather = "Snow fall: Moderate";
					break;
				case 75:
					kindOfWeather = "Snow fall: Heavy intensity";
					break;
				case 77:
					kindOfWeather = "Snow grains";
					break;
				case 80:
					kindOfWeather = "Rain showers: Slight";
					break;
				case 81:
					kindOfWeather = "Rain showers: Moderate";
					break;
				case 82:
					kindOfWeather = "Rain showers: Violent";
					break;
				case 85:
					kindOfWeather = "Snow showers slight";
					break;
				case 86:
					kindOfWeather = "Snow showers heavy";
					break;
				case 95:
					kindOfWeather = "Thunderstorm: Slight to Moderate";
					break;
				case 96:
					kindOfWeather = "Thunderstorm with slight hail";
					break;
				case 99:
					kindOfWeather = "Thunderstorm with heavy hail";
					break;
				default:
					kindOfWeather = "Unable to Fetch WeatherConditions";
					break;
			}
			return kindOfWeather;
		}
	}
}
