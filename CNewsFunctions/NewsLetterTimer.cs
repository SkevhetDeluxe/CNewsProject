using System;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using CNewsFunctions.Data;
using CNewsFunctions.Services;
using CNewsFunctions.Models;
using CNewsProject.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;

namespace CNewsFunctions;

public class NewsLetterTimer(ILogger<NewsLetterTimer> logger, FunctionDbContext functionDbContext, QueueServiceClient queueServiceClient, INewsLetterService _service)
{

    [Function("NewsLetterTimer")]
    public void Run([TimerTrigger("0 0 6 * * 1", RunOnStartup = true)] TimerInfo myTimer)
    {
        logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        
        // Fetching USERS
        var users = _service.GetEmailUserList();
        
        // Fetching RECENT ArticleList
        var recentArticles = _service.GetRecentArticleList();
        
        // Constructing The Instructions
        List<EmailInstruction> emailInstructions = new();
        foreach (var user in users)
        {
            emailInstructions.Add(new EmailInstruction()
            {
                Email = user.Email,
                UserName = user.UserName,
                Subject = "Weekly News Letter",
                ArticleIds = _service.GetUserNewsLetterArticles(user, recentArticles),
                AuthorNames = user.AuthorNames ?? new(),
            });
        }
        
        QueueClient queueClient = queueServiceClient.GetQueueClient("newsletterlist");
        //queueClient.SendMessage("IT WORKED WITH THIS QUEUE");
        
        // SENDING the INSTRUCTIONS!!!
        foreach (var instruction in emailInstructions)
        {
            queueClient.SendMessage((JsonConvert.SerializeObject(instruction)));
        }
        
        //queueClient = queueServiceClient.GetQueueClient("clearpoppin");
        queueClient.SendMessage(""); // TODO OR NOT TODO? THAT IS THE QUESTION! SEND INSTRUCTIONS TO CLEAR THE WEEKLY VIEWS!

        // TESTING ONLY
        // EmailInstruction testInstruction = new()
        // {
        //     Email = "filip.bergfjord@protonmail.com",
        //     UserName = "Filip",
        //     Subject = "Weekly Newsletter",
        //     ArticleIds = _service.GetLatestArticles(),
        //     AuthorNames = new(),
        // };

        // queueClient.SendMessage(JsonConvert.SerializeObject(testInstruction));
    }
}