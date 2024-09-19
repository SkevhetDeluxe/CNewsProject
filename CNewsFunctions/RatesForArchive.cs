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
        public DateTime DateUpdated { get; set; }

        [Range(0, double.MaxValue)]
        public decimal USD { get; set; }

        [Range(0, double.MaxValue)]
        public decimal EUR { get; set; }

        [Range(0, double.MaxValue)]
        public decimal GBP { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public RatesForArchive() { }

        public RatesForArchive(DateTime dateUpdated)
        {
            PartitionKey = "RatesArchive";  
            RowKey = dateUpdated.ToString("yyyyMMdd"); 
            DateUpdated = dateUpdated;
        }
    }
}
