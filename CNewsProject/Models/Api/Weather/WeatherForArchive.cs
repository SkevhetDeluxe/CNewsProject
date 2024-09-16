using System;
using Azure;
using Azure.Data.Tables;

namespace CNewsProject.Models.Api.Weather
{
    public class WeatherForArchive : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public DateTime DateUpdated { get; set; }
        public float Temperature { get; set; }
        public string Condition { get; set; }

        public WeatherForArchive() { }

        public WeatherForArchive(DateTime dateUpdated, float temperature, string condition)
        {
            PartitionKey = "WeatherData";
            RowKey = dateUpdated.ToString("yyyyMMddHHmmss");
            DateUpdated = dateUpdated;
            Temperature = temperature;
            Condition = condition;
        }
    }
}
