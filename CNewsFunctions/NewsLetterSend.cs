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

public class NewsLetterSend(ILogger<NewsLetterSend> logger, IConfiguration configuration)
{
    [Function(nameof(NewsLetterSend))]
    public void Run([QueueTrigger("newsletterlist", Connection = "AzureWebJobsStorage")] QueueMessage message)
    {
        if (string.IsNullOrEmpty(message.MessageText))
        {
            logger.LogInformation($"C# REMOVING NULL MESSAGE {message}");
            return;
        }

        EmailInstruction emailInstruction = JsonConvert.DeserializeObject<EmailInstruction>(message.MessageText)!;

        EmailHelper emailHelper = new(configuration);

        emailHelper.SendEmailInstructionAsync(emailInstruction);
        
        logger.LogInformation($"C# Queue trigger function processed: email = {message.MessageText}");
        
    }
}