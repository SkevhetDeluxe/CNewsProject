using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using System;
using System.ComponentModel.DataAnnotations;

namespace CNewsFunctions
{
    public class RatesForArchive:ITableEntity
    {        
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }        
        public DateOnly DateUpdated { get; set; }       
        public decimal USD { get; set; }
        public decimal EUR { get; set; }
        public decimal GBP { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public RatesForArchive() { }

        public RatesForArchive(DateTime dateUpdated)
        {
            PartitionKey = "RatesArchive";  // Could be a fixed partition
            RowKey = dateUpdated.ToString("yyyyMMdd"); // RowKey based on date for uniqueness
            DateUpdated = DateOnly.FromDateTime(dateUpdated);
        }
    }
}
