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

public class NewsLetterTimer(
    ILogger<NewsLetterTimer> logger,
    FunctionDbContext functionDbContext,
    QueueServiceClient queueServiceClient,
    ISuperService service,
    IConfiguration config)
{
    //const string schedule = "0 0 6 * * 1";
    private static readonly string TestMode = Environment.GetEnvironmentVariable("CNEWS_NEWSLETTER_TESTMODE") ?? "0";

    [Function("NewsLetterTimer")]
    public void Run([TimerTrigger("0 0 5 * * 1", UseMonitor = true)] TimerInfo myTimer)
    {
        QueueClient queueClient = queueServiceClient.GetQueueClient("devqueuetwo");
        queueClient.SendMessage($"NewsLetterTimer ACTIVATED at {DateTime.Now}");

        string schedule = Environment.GetEnvironmentVariable("CNEWS_NEWSLETTER_SCHEDULE") ?? "";

        logger.LogInformation($"CNEWS_NEWSLETTER_TESTMODE is set at {TestMode}");

        if (TestMode == "1")
        {
            TestRun();
        }

        logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

        // Fetching USERS
        var users = service.GetEmailUserList();

        // Fetching RECENT ArticleList
        var recentArticles = service.GetRecentArticleList();

        // Constructing The Instructions
        List<EmailInstruction> emailInstructions = new();
        int count = 0;
        int totalCount = users.Count();
        if (totalCount == 0)
        {
            foreach (var user in users)
            {
                emailInstructions.Add(new EmailInstruction()
                {
                    AmountOfMessages = totalCount,
                    NumberInList = count++,
                    Email = user.Email,
                    UserName = user.UserName,
                    Subject = "Weekly News Letter",
                    ArticleIds = service.GetUserNewsLetterArticles(user, recentArticles),
                    AuthorNames = user.AuthorNames ?? new(),
                });
            } 
        }
        else
        {
            emailInstructions.Add(new EmailInstruction()
            {
                AmountOfMessages = 1,
                NumberInList = 0,
                Email = "NOMAIL",
                UserName = "IGNORE",
                Subject = "IGNORE",
                ArticleIds = new(),
                AuthorNames = new()
            });   
        }
        
        queueClient = queueServiceClient.GetQueueClient("newsletterlist");
        // SENDING the INSTRUCTIONS!!!
        foreach (var instruction in emailInstructions)
        {
            queueClient.SendMessage((JsonConvert.SerializeObject(instruction)));
        }

        // queueClient.SendMessage(JsonConvert.SerializeObject(testInstruction));
    }

    private void TestRun()
    {
        logger.LogInformation("Testing mode");
        EmailInstruction testInstruction = new()
        {
            Email = "filip.bergfjord@protonmail.com",
            UserName = "Filip",
            Subject = "Weekly Newsletter",
            ArticleIds = service.GetLatestArticles(),
            AuthorNames = new(),
        };
        QueueClient queueClient = queueServiceClient.GetQueueClient("devqueueone");

        queueClient.SendMessage((JsonConvert.SerializeObject(testInstruction)));
    }
}