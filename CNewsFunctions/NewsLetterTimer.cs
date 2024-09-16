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
using Microsoft.Extensions.Configuration;

namespace CNewsFunctions;

public class NewsLetterTimer(ILogger<NewsLetterTimer> logger, FunctionDbContext functionDbContext, QueueClient queueClient, INewsLetterService _service)
{

    [Function("NewsLetterTimer")]
    public void Run([TimerTrigger("0 0 7 * * 1", RunOnStartup = true)] TimerInfo myTimer)
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
                Subject = "Weekly Newsletter",
                ArticleIds = _service.GetUserNewsLetterArticles(user, recentArticles),
                AuthorNames = user.AuthorNames ?? new(),
            });
        }
        
        // SENDING the INSTRUCTIONS!!!
        foreach (var instruction in emailInstructions)
        {
            queueClient.SendMessage((JsonConvert.SerializeObject(instruction)));
        }
    }
}