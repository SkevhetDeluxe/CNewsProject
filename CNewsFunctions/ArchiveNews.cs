using System;
using CNewsProject.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CNewsFunctions;

public class ArchiveNews(ILogger<ArchiveNews> logger, ApplicationDbContext db)
{
    [Function("ArchiveNews")] // Assign False to RunOnStartup Parameter before PUBLISHING
    public void Run([TimerTrigger("0/30 * * * * *", RunOnStartup = true)] TimerInfo myTimer)
    {
        logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        
        var articles = db.Article.ToList();
        var toArchive = articles.Where(a => a.PublishedDate <= DateTime.Now.AddDays(-30)).ToList();
        var toArchiveIds = toArchive.Select(a => a.Id).ToList();

        foreach (var id in toArchiveIds)
        {
            db.Article.Find(id)!.IsArchived = true;
        }

        db.SaveChanges();
        
        logger.LogInformation($"Articles archived: {toArchiveIds.Count()}");
        
        if (myTimer.ScheduleStatus is not null)
        {
            logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            
        }
    }
}