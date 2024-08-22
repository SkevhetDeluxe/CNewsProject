namespace CNewsProject.Models.Api.Weather
{

    public class GeoLocation
    {
        public string name { get; set; }
        public Localnames localNames { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public string country { get; set; }
    }

    public class Localnames
    {
        public string th { get; set; }
        public string lv { get; set; }
        public string mk { get; set; }
        public string el { get; set; }
        public string la { get; set; }
        public string ar { get; set; }
        public string sr { get; set; }
        public string fa { get; set; }
        public string lt { get; set; }
        public string ru { get; set; }
        public string zh { get; set; }
        public string uk { get; set; }
        public string bg { get; set; }
        public string ja { get; set; }
        public string be { get; set; }
        public string sv { get; set; }
        public string ko { get; set; }
        public string hy { get; set; }
        public string ka { get; set; }
    }


}

//Code	 Description

//int code = Hourly[];
//string kindOfWeather;

//switch (code)
//{
//	case 0:
//		kindOfWeather = "Clear sky";
//		break;
//	case 1:
//		kindOfWeather = "Mainly clear";
//		break;
//	case 2:
//		kindOfWeather = "Partly cloudy";
//		break;
//	case 3:
//		kindOfWeather = "Overcast"; ;
//		break;
//	case 45:
//		kindOfWeather = "Fog";
//		break;
//	case 48:
//		kindOfWeather = "Depositing rime fog";
//		break;
//	case 51:
//		kindOfWeather = "Drizzle: Light";
//		break;
//	case 53:
//		kindOfWeather = "Drizzle: Moderate";
//		break;
//	case 55:
//		kindOfWeather = "Drizzle: Dense intensity";
//		break;
//	case 56:
//		kindOfWeather = "Freezing Drizzle: Light";
//		break;
//	case 57:
//		kindOfWeather = "Freezing Drizzle: Dense intensity";
//		break;
//	case 61:
//		kindOfWeather = "Rain: Slight";
//		break;
//	case 63:
//		kindOfWeather = "Rain: Moderate";
//		break;
//	case 65:
//		kindOfWeather = "Rain: Heavy intensity";
//		break;
//	case 66:
//		kindOfWeather = "Freezing Rain: Light";
//		break;
//	case 67:
//		kindOfWeather = "Freezing Rain: Heavy intensity";
//		break;
//	case 71:
//		kindOfWeather = "Snow fall: Slight";
//		break;
//	case 73:
//		kindOfWeather = "Snow fall: Moderate";
//		break;
//	case 75:
//		kindOfWeather = "Snow fall: Heavy intensity";
//		break;
//	case 77:
//		kindOfWeather = "Snow grains";
//		break;
//	case 80:
//		kindOfWeather = "Rain showers: Slight";
//		break;
//	case 81:
//		kindOfWeather = "Rain showers: Moderate";
//		break;
//	case 82:
//		kindOfWeather = "Rain showers: Violent";
//		break;
//	case 85:
//		kindOfWeather = "Snow showers slight";
//		break;
//	case 86:
//		kindOfWeather = "Snow showers heavy";
//		break;
//	case 95:
//		kindOfWeather = "Thunderstorm: Slight to Moderate";
//		break;
//	case 96:
//		kindOfWeather = "Thunderstorm with slight hail";
//		break;
//	case 99:
//		kindOfWeather = "Thunderstorm with heavy hail";
//		break;

//}
