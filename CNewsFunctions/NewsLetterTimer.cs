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

public class NewsLetterTimer(ILogger<NewsLetterTimer> logger, FunctionDbContext functionDbContext, QueueServiceClient queueServiceClient, ISuperService _service)
{
    //const string schedule = "0 0 6 * * 1";
    private readonly string _testMode = Environment.GetEnvironmentVariable("CNEWS_NEWSLETTER_TESTMODE")!;
    [Function("NewsLetterTimer")] // TODO REMOVE                   RunOnStartup!!!!!!!
    public void Run([TimerTrigger("%CNEWS_NEWSLETTER_SCHEDULE%", UseMonitor = true)] TimerInfo myTimer)
    {
        logger.LogInformation($"CNEWS_NEWSLETTER_TESTMODE is set at {_testMode}");
        
        if (_testMode == "1")
        {
            TestMode();
        }
        else
        {
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        
            // Fetching USERS
            var users = _service.GetEmailUserList();
        
            // Fetching RECENT ArticleList
            var recentArticles = _service.GetRecentArticleList();
        
            // Constructing The Instructions
            List<EmailInstruction> emailInstructions = new();
            int count = 0;
            int totalCount = users.Count();
            foreach (var user in users)
            {
                emailInstructions.Add(new EmailInstruction()
                {
                    AmountOfMessages = totalCount,
                    NumberInList = count++,
                    Email = user.Email,
                    UserName = user.UserName,
                    Subject = "Weekly News Letter",
                    ArticleIds = _service.GetUserNewsLetterArticles(user, recentArticles),
                    AuthorNames = user.AuthorNames ?? new(),
                });
            }
        
            QueueClient queueClient = queueServiceClient.GetQueueClient("newsletterlist");
            // SENDING the INSTRUCTIONS!!!
            foreach (var instruction in emailInstructions)
            {
                queueClient.SendMessage((JsonConvert.SerializeObject(instruction)));
            } 
        }
        // queueClient.SendMessage(JsonConvert.SerializeObject(testInstruction));
    }

    private void TestMode()
    {
        logger.LogInformation("Testing mode");
        EmailInstruction testInstruction = new()
        {
            Email = "filip.bergfjord@protonmail.com",
            UserName = "Filip",
            Subject = "Weekly Newsletter",
            ArticleIds = _service.GetLatestArticles(),
            AuthorNames = new(),
        };
        QueueClient queueClient = queueServiceClient.GetQueueClient("devqueueone");
        
        queueClient.SendMessage((JsonConvert.SerializeObject(testInstruction)));
    }
}