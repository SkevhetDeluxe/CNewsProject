using System.Runtime.CompilerServices;

namespace CNewsProject.Models.Api.Weather
{
	public class Tables
	{

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
					kindOfWeather = "Overcast";
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
			
			return kindOfWeather ;

		}

		public string GetDirectionFromInt(int windDir)
		{
			string windDirection="North";
			if (windDir < 79)
			{
				if (windDir <= 12)
					windDirection = "North";
				else if (windDir >= 13 && windDir <= 33)
					windDirection = "North, northeast";
				else if (windDir >= 34 && windDir <= 56)
					windDirection = "Northeast";
				else if (windDir >= 57 && windDir <= 78)
					windDirection = "East, northeast";
			}
			else if (windDir < 169)
			{
				if (windDir >= 79 && windDir <= 102)
					windDirection = "East";
				else if (windDir >= 103 && windDir <= 123)
					windDirection = "East, southeast";
				else if (windDir >= 124 && windDir <= 147)
					windDirection = "Southeast";
				else if (windDir >= 148 && windDir <= 168)
					windDirection = "South, southeast";
			}
			else if (windDir < 258)
			{
				if (windDir >= 169 && windDir <= 192)
					windDirection = "South";
				else if (windDir >= 193 && windDir <= 213)
					windDirection = "South, southwest";
				else if (windDir >= 214 && windDir <= 236)
					windDirection = "Southwest";
				else if (windDir >= 237 && windDir <= 257)
					windDirection = "West, southwest";
			}
			else
			{
				if (windDir >= 258 && windDir <= 281)
					windDirection = "West";
				else if (windDir >= 282 && windDir <= 302)
					windDirection = "West, northwest";
				else if (windDir >= 303 && windDir <= 326)
					windDirection = "Northwest";
				else if (windDir >= 327 && windDir <= 347)
					windDirection = "North, northwest";
				else if (windDir >= 348 && windDir <= 360)
					windDirection = "North";
			}
			return windDirection;

		}

		public string GetSpeedFromFloat(float windStr)
		{
			string windSpeed ="Calm";
			if (windStr < 3.4)
			{
				if (windStr <= 0.2)
					windSpeed = "Calm";
				else if (windStr >= 0.3 && windStr <= 1.5)
					windSpeed = "Light Air";
				else if (windStr >= 1.6 && windStr <= 3.3)
					windSpeed = "Light Breeze";
			}
			else if (windStr < 10.8)
			{
				if (windStr >= 3.4 && windStr <= 5.4 )
					windSpeed = "Gentle Breeze";
				else if (windStr >= 5.5 && windStr <= 7.9)
					windSpeed = "Moderate Breeze";
				else if (windStr >= 8.0 && windStr <= 10.7)
					windSpeed = "Fresh Breeze";
			}
			else if (windStr < 20.8)
			{
				if (windStr >= 10.8 && windStr <= 13.8)
					windSpeed = "Strong Breeze";
				else if (windStr >= 13.9 && windStr <= 17.1)
					windSpeed = "Near Gale";
				else if (windStr >= 17.2 && windStr <= 20.7)
					windSpeed = "Gale";
			}
			else if (windStr < 258)
			{
				if (windStr >= 20.8 && windStr <= 24.4)
					windSpeed = "Severe Gale";
				else if (windStr >= 24.5 && windStr <= 28.4)
					windSpeed = "Storm";
				else if (windStr >= 28.5 && windStr <= 32.6)
					windSpeed = "Violent Storm";
				else if (windStr >= 32.7)
					windSpeed = "Hurricane";
			}

			return windSpeed;


		}

		public string GetWeatherPicFromInt(int condition , int sun)
		{
			string weatherPic ="";
			
			//Code	 Description
			if (sun == 1)
			{ 
				switch (condition)
				{
				case 0:
					weatherPic = "day/Clear.png";
					break;
				case 1:
					weatherPic = "day/MainlyClear.png";
					break;
				case 2:
					weatherPic = "day/SemiClear.png";
					break;
				case 3:
					weatherPic = "day/ALotOfCloud.png"; 
					break;
				case 45:
					weatherPic = "day/Fog.png";
					break;
				case 48:
					weatherPic = "day/Fog.png";
					break;
				case 51:
					weatherPic = "day/LightRainShower.png";		//Duggregn : Drizzle
					break;
				case 53:
					weatherPic = "day/RainShower.png";          //Duggregn : Drizzle
						break;
				case 55:
					weatherPic = "day/HeavyRainShower.png";     //Duggregn : Drizzle
						break;
				case 56:
					weatherPic = "day/LightRainShower.png";     //Duggregn : Drizzle cold
						break;
				case 57:
					weatherPic = "day/HeavyRainShower.png";     //Duggregn : Drizzle cold
						break;
				case 61:
					weatherPic = "day/LightRain.png.png";
					break;
				case 63:
					weatherPic = "day/Rain.png";
					break;
				case 65:
					weatherPic = "day/HeavyRain.png";
						break;
				case 66:
					weatherPic = "day/LightSleet.png";           // Snöblandat regn
						break;
				case 67:
					weatherPic = "day/HeavySleet.png";           // Snöblandat regn
						break;
				case 71:
					weatherPic = "day/LightSnowfall.png";
					break;
				case 73:
					weatherPic = "day/Snowfall.png";
					break;
				case 75:
					weatherPic = "day/HeavySnowfall.png";
						break;
				case 77:
					weatherPic = "Snow grains";                     //--
						break;
				case 80:
					weatherPic = "day/LightRainShower.png";
					break;
				case 81:
					weatherPic = "day/RainShower.png";
					break;
				case 82:
					weatherPic = "day/HeavyRainShower.png";
					break;
				case 85:
					weatherPic = "day/SnowShower.png";
						break;
				case 86:
					weatherPic = "day/HeavySnowShower.png";
						break;
				case 95:
					weatherPic = "day/ThunderShower.png";
						break;
				case 96:
					weatherPic = "Thunderstorm with slight hail";           //--
						break;
				case 99:
					weatherPic = "Thunderstorm with heavy hail";            //--
						break;
				default:
					weatherPic = "Unable to Fetch WeatherConditions";
					break;
				}
				
			}
			else if (sun == 2)
			{
				switch (condition)
				{
					case 0:
						weatherPic = "night/ClearNight.png";
						break;
					case 1:
						weatherPic = "night/MainlyClear.png";
						break;
					case 2:
						weatherPic = "night/PartlyCloudyNight.png";
						break;
					case 3:
						weatherPic = "night/Overcast.png";
						break;
					case 45:
						weatherPic = "day/Fog.png";
						break;
					case 48:
						weatherPic = "day/Fog.png";
						break;
					case 51:
						weatherPic = "night/CloudyNight.png";
						break;
					case 53:
						weatherPic = "Drizzle: Moderate";               //--
						break;
					case 55:
						weatherPic = "Drizzle: Dense intensity";        //--
						break;
					case 56:
						weatherPic = "Freezing Drizzle: Light";         //--
						break;
					case 57:
						weatherPic = "Freezing Drizzle: Dense intensity";       //--
						break;
					case 61:
						weatherPic = "day/LightRain.png.png";
						break;
					case 63:
						weatherPic = "day/Rain.png";
						break;
					case 65:
						weatherPic = "day/HeavyRain.png";
						break;
					case 66:
						weatherPic = "day/LightSleet.png";           // Snöblandat regn
						break;
					case 67:
						weatherPic = "day/HeavySleet.png";           // Snöblandat regn
						break;
					case 71:
						weatherPic = "night/LightSnowfall.png";
						break;
					case 73:
						weatherPic = "day/Snowfall.png";
						break;
					case 75:
						weatherPic = "day/HeavySnowfall.png";         
						break;
					case 77:
						weatherPic = "Snow grains";                         //--
						break;
					case 80:
						weatherPic = "night/LightRainShowerNight.png";				//"Rain showers: Slight";
						break;
					case 81:
						weatherPic = "night/RainShowerNight.png";
						break;
					case 82:
						weatherPic = "Rain showers: Violent";               //--
						break;
					case 85:
						weatherPic = "night/LightSnowFallNight.png";
						break;
					case 86:
						weatherPic = "Snow showers heavy";                  //--
						break;
					case 95:
						weatherPic = "night/ThunderShower.png";				//"Thunderstorm: Slight to Moderate";
						break;
					case 96:
						weatherPic = "Thunderstorm with slight hail";       //--
						break;
					case 99:
						weatherPic = "Thunderstorm with heavy hail";        //--
						break;
					default:
						weatherPic = "Unable to Fetch WeatherConditions";
						break;
				}
				
			}
			return weatherPic;
		}
	}
}
