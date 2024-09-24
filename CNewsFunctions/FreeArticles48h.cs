using System;
using CNewsFunctions.Data;
using CNewsFunctions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CNewsFunctions;

public class FreeArticles48H(ILogger<FreeArticles48H> logger, FunctionDbContext db) // TODO PUBLISH
{
    [Function("FreeArticles48h")]
    public void Run([TimerTrigger("0 0 * * * *", RunOnStartup = true)] TimerInfo myTimer)
    {
        logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

        Tuple<bool, int> result = SetFreeArticles();

        if (result.Item1)
        {
            logger.LogInformation($"{result.Item2} more articles are available for free.");
        }

        if (myTimer.ScheduleStatus is not null)
        {
            logger.LogInformation($"Will check for ARTICLES to be SET FREE again at {myTimer.ScheduleStatus.Next}");
        }
        
    }

    private Tuple<bool, int> SetFreeArticles()
    {
        try
        {
            var allArticles = db.Article.ToList();
            var potentialArticles = allArticles
                .Where(a => a.IsntNotFree == false && a.Status == "Approved").ToList();

            int count = 0;
            foreach (Article article in potentialArticles)
            {
                if (DateTime.Now - article.PublishedDate >= TimeSpan.FromHours(48))
                {
                    article.IsntNotFree = true;
                    count++;
                }
            }

            db.SaveChanges();
            return new Tuple<bool, int>(true, count);
        }
        catch(Exception ex)
        {
            logger.LogInformation("CODE ERROR");
            logger.LogError(ex, ex.Message);
            return new Tuple<bool, int>(false, 0);
        }
    }
}