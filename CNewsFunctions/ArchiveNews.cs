using System;
using CNewsProject.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CNewsFunctions;

public class ArchiveNews(ILogger<ArchiveNews> logger, FunctionDbContext db, IConfiguration config)
{
    [Function("ArchiveNews")] // Assign False to RunOnStartup Parameter before PUBLISHING
    public void Run([TimerTrigger("0 0 * * * * ", RunOnStartup = false)] TimerInfo myTimer)
    {
        logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        
        var articles = db.Article.Where(a => a.Status == "Approved").ToList();
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

// STATUSES FOR ARTICLES
/*
    Approved = Saved in DB. Users can READ
    Draft = Saved in DB. Not readable by anyone else but Author.
    Declined = Saved in DB. Only readable by author and needs to be resubmitted.

*/
