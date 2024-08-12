using CNewsProject.Models.DataBase;
using System;
namespace CNewsProject.Service
{
    public class ArticleLockService : IArticleLockService
    {
        public bool IsArticleLocked(Article article)
        {
            TimeSpan timeSincePublished = DateTime.Now - article.PublishedDate;

            
            return timeSincePublished.TotalHours > 48;
        }
    }
}
