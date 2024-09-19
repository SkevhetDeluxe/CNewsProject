using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace CNewsFunctions
{
    public class WeatherForArchive:ITableEntity
    {        
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateOnly DateUpdated { get; set; }

        [Range(-100, 100)]
        public float Temperature { get; set; }     // Temperature value
        [Required]
        public string Condition { get; set; }      // Weather condition (e.g., sunny, rainy)

        // Required properties for Table Storage
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        // Constructor
        public WeatherForArchive() { }

        // Convenience constructor to easily create a new entity
        public WeatherForArchive(DateTime dateUpdated, float temperature, string condition)
        {
            PartitionKey = "WeatherArchive";  // Static partition for all weather data
            RowKey = dateUpdated.ToString("yyyyMMdd-HHmmss");  // Unique row key based on timestamp

            DateUpdated = DateOnly.FromDateTime(dateUpdated);  // Convert DateTime to DateOnly
            Temperature = temperature;
            Condition = condition;
        }
    }
}
