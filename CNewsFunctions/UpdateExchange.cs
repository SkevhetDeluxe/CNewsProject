using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CNewsFunctions
{
    public class UpdateExchange
    {
        private readonly ILogger _logger;

        private string token = "HawiIUdjoAIDJolaijiowjdOIAJDlAJWDNkjnd2ui82doOI";

        public UpdateExchange(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UpdateExchange>();
        }

        [Function("UpdateExchange")]
        public void Run([TimerTrigger("*/30 * * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"Entered TimerTest at Time: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
            
            using (HttpClient client = new HttpClient())
            {
                client.GetAsync("https://localhost:44374/LoadExchange/UpdateExchange?token=" + token);
                _logger.LogInformation($"Sent Request to UpdateExchange at Time: {DateTime.Now}");
            }

            string hej = "HEJ På DIG";
        }
    }
}
