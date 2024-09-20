namespace CNewsProject.Models.Api.Weather
{

    public class GeoLocation
    {
        public string? name { get; set; }
        public Localnames? localNames { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public string? country { get; set; }
    }

    public class Localnames
    {
        public string? th { get; set; }
        public string? lv { get; set; }
        public string? mk { get; set; }
        public string? el { get; set; }
        public string? la { get; set; }
        public string? ar { get; set; }
        public string? sr { get; set; }
        public string? fa { get; set; }
        public string? lt { get; set; }
        public string? ru { get; set; }
        public string? zh { get; set; }
        public string? uk { get; set; }
        public string? bg { get; set; }
        public string? ja { get; set; }
        public string? be { get; set; }
        public string? sv { get; set; }
        public string? ko { get; set; }
        public string? hy { get; set; }
        public string? ka { get; set; }
    }
	
}
