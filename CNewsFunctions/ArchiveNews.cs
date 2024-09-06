using System;
using CNewsProject.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CNewsFunctions;

public class ArchiveNews(ILogger<ArchiveNews> logger, FunctionDbContext db)
{
    [Function("ArchiveNews")]
    public void Run([TimerTrigger("0 0 * * 5")] TimerInfo myTimer)
    {
        logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

        var articles = db.Article.ToList();
        var toArchive = articles.Where(a => a.PublishedDate <= DateTime.Now.AddDays(-30)).ToList();
        //var toArchiveIds = toArchive.Select(a => a.Id).ToList();

        foreach (var article in toArchive)
        {
            article.Status = "Archived";
        }

        db.SaveChanges();

        logger.LogInformation($"Articles archived: {toArchive.Count()}");

        if (myTimer.ScheduleStatus is not null)
        {
            logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }
}