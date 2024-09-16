using CNewsFunctions.Data;
using CNewsFunctions.Models;
using Microsoft.EntityFrameworkCore;

namespace CNewsFunctions.Services;

public class NewsLetterService(FunctionDbContext dbContext) : INewsLetterService
{
    public List<AppUser> GetEmailUserList()
    {
        return dbContext.Users.Where(u => u.NewsLetterEnabled).ToList();
    }

    public List<Article> GetRecentArticleList()
    {        
        DateTime theWeek = DateTime.Now.AddDays(-7);
        return dbContext.Article.Where(a => a.PublishedDate > theWeek).ToList();
    }

    public List<int> GetUserNewsLetterArticles(AppUser user, List<Article> allArticles)
    {
        var catIds = user.CategoryIds;
        var latest = user.Latest;
        var popular = user.Popular;
        var authors = user.AuthorNames;

        List<int> articleIds = new();


        // if (authors != null)
        // {
        //     articleIds.AddRange(allArticles
        //         .Where(a => authors.Contains(a.AuthorUserName))
        //         .Select(a => a.Id)
        //         .ToList());
        // }

        if (latest)
        {
            articleIds.AddRange(allArticles.Where(a => !articleIds.Contains(a.Id))
                .Where(a => catIds.Contains(a.CategoryId))
                .Select(a => a.Id)
                .Take(3).ToList());
        }

        if (popular)
        {
            // TODO Add Popular Settings and Shit in MAIN PROGRAM
        }

        return articleIds;
    }

    public string ConstructHtmlBody(EmailInstruction instruction)
    {
        string body = "<html><body>";
        return "FUCK YOU";
    }
}

public interface INewsLetterService
{
    public List<AppUser> GetEmailUserList();
    public List<Article> GetRecentArticleList();
    public List<int> GetUserNewsLetterArticles(AppUser user, List<Article> allArticles);
    public string ConstructHtmlBody(EmailInstruction instruction);
}