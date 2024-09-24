using System;
using Azure.Storage.Queues.Models;
using Azure.Storage.Queues;
using CNewsFunctions.Models;
using CNewsProject.Helpers;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CNewsFunctions;

public class NewsLetterSend(ILogger<NewsLetterSend> logger, IConfiguration configuration, QueueServiceClient queueServiceClient)
{
    private List<int> AmountIncoming { get; set; } = new();
    private bool Refilled { get; set; } = false;

    [Function(nameof(NewsLetterSend))]
    public void Run([QueueTrigger("newsletterlist", Connection = "AzureWebJobsStorage")] QueueMessage message)
    {
        if (string.IsNullOrEmpty(message.MessageText))
        {
            logger.LogInformation($"C# REMOVING NULL MESSAGE {message}");
            return;
        }
        EmailInstruction emailInstruction = JsonConvert.DeserializeObject<EmailInstruction>(message.MessageText)!;

        if (!Refilled)
        {
            for (int i = 0; i < emailInstruction.AmountOfMessages; i++)
            {
                AmountIncoming.Add(i);
            }
            Refilled = true;
        }

        AmountIncoming.Remove(emailInstruction.NumberInList);
        
        EmailHelper emailHelper = new(configuration);

        emailHelper.SendEmailInstructionAsync(emailInstruction);
        
        logger.LogInformation($"C# Queue trigger function processed: email = {message.MessageText}");

        if (AmountIncoming.Count == 0)
        {
            QueueClient queueClient = queueServiceClient.GetQueueClient("clearpoppin");
            queueClient.SendMessage("CLEAR POPPIN");
            
            Refilled = false;
        }
    }
}