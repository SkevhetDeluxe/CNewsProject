using System;
using Azure.Storage.Queues.Models;
using CNewsFunctions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CNewsFunctions;

public class ClearPoppin(ILogger<ClearPoppin> logger, ISuperService superService)
{
    [Function(nameof(ClearPoppin))]
    public void Run([QueueTrigger("clearpoppin", Connection = "AzureWebJobsStorage")] QueueMessage message)
    {
        if (message.MessageText == "CLEAR POPPIN")
        {
            logger.LogInformation("INITIATE CLEAR POPPIN");
            logger.LogInformation("INITIATING CLEAR POPPIN");
            logger.LogInformation("INITIATED CLEAR POPPIN");
            logger.LogInformation(superService.Clear("POPPIN"));
            logger.LogInformation("YES SIR, CLEARING POPPIN");
            logger.LogInformation("SIR WE CLEARED POPPIN");
        }
    }
}