using Microsoft.AspNetCore.Mvc.Rendering;
using CNewsProject.Models.DataBase;
using CNewsProject.Data;
using CNewsProject.Models.ViewModels;

namespace CNewsProject.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public ArticleService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        #region Base_Methods()
        public List<Article> GetAllArticles()
        {
            return _db.Article.OrderBy(m => m.Headline).ToList();
        }

        public Article GetArticleById(int Id)
        {
            return _db.Article.FirstOrDefault(m => m.Id == Id);
        }

        public void AddArticle(Article article)
        {
            _db.Article.Add(article);
            _db.SaveChanges();
        }

        public void RemoveArticle(Article article)
        {
            _db.Article.Remove(_db.Article.FirstOrDefault(m => m.Id == article.Id));
            _db.SaveChanges();
        }




        public void EditArticle (Article article)
        {
            GetArticleById(article.Id).DateStamp = article.DateStamp;
            GetArticleById(article.Id).LinkText = article.LinkText;
            GetArticleById(article.Id).Headline = article.Headline;
            GetArticleById(article.Id).ContentSummary = article.ContentSummary;
            GetArticleById(article.Id).Content = article.Content;
            GetArticleById(article.Id).Views = article.Views;
            GetArticleById(article.Id).Likes = article.Likes;
            GetArticleById(article.Id).ImageLink = article.ImageLink;
            GetArticleById(article.Id).Category = article.Category;
            GetArticleById(article.Id).IsArchived = article.IsArchived;

            _db.SaveChanges();
        }



        #endregion


        #region Get_Lists_With_Filters()
        // Overload later to take filters and sortings
        public List<Article> GetArticleList()
        {
            List<Article> articleList = GetAllArticles();

            return articleList;
        }

        public List<Article> GetArticleList(ArticleListVM vModel)
        {
            List<Article> articleList = GetAllArticles();

            if (vModel.Headline != "All")
                articleList = articleList.Where(m => m.Headline == vModel.Headline).ToList();

            if (vModel.Category != "All")
                articleList = articleList.Where(m => m.Category == vModel.Category).ToList();

            return articleList;
        }


        // Get SORTINGS and FILTERS
        public List<string> GetHeadlines() // Gets a List<string> of Headlines. No Dupes.
        {
            List<string> headlines = new()
            {
                "All"
            };

            foreach (Article article in GetAllArticles())
            {
                if (!headlines.Contains(article.Headline))
                    headlines.Add(article.Headline);
            }

            return headlines;
        }

        public List<string> GetCategories() // Gets a List<string> of Categories. No Dupes.
        {
            List<string> categories = new()
            {
                "All"
            };

            foreach (Article article in GetAllArticles())
            {
                if (!categories.Contains(article.Category))
                    categories.Add(article.Category);
            }

            return categories;
        }

        // SELECT LISTS
        public List<SelectListItem> GetHeadlineList()
        {
            List<SelectListItem> headlineList = new();

            foreach (string headline in GetHeadlines())
            {
                headlineList.Add(new SelectListItem
                {
                    Value = headline,
                    Text = headline
                });
            }

            return headlineList;
        }

        public List<SelectListItem> GetCategoryList()
        {
            List<SelectListItem> categoryList = new();

            foreach (string category in GetCategories())
            {
                categoryList.Add(new SelectListItem
                {
                    Value = category,
                    Text = category
                });
            }

            return categoryList;
        }
        #endregion


        #region Specifics_For_FrontPage()
        // These Methods are for displaying TOP Fives on the front page

        //public List<Article> FPMostPopular()
        //{
        //    var fpmostpopular = _db.OrderRows.GroupBy(or => or.ArticleId)
        //       .OrderByDescending(g => g.Count())
        //       .Select(m => new Article()
        //       {
        //           Id = m.Key,
        //           Headline = m.First().Article.Headline,
        //           Views = m.First().Article.Views,
        //           Likes = m.First().Article.Likes,

        //       }).Take(5).ToList();

        //    return fpmostpopular;
        //}


        public List<Article> FPNewest()
        {
            List<Article> articleList = new();
            articleList = GetAllArticles().OrderByDescending(m => m.DateStamp)
                .Take(5).ToList();

            return articleList;
        }
        public List<Article> FPOldest()
        {
            List<Article> articleList = new();
        articleList = GetAllArticles().OrderBy(m => m.DateStamp)
                .Take(5).ToList();

            return articleList;
        }
        //public List<Article> FPCheapest()
        //{
        //    List<Article> articleList = new();
        //articleList = GetAllArticles().OrderBy(m => m.Price)
        //        .Take(5).ToList();

        //    return articleList;
        //}

        #region SetupVM()_For_FrontPage_Specifics

        // Since FrontPageVM carries a List<List<Movie>> it NEEDS to be
        // filled and displayed in the same order EVERY time or you will 
        // get the wrong lists on the wrong places.

        /* Order is
         * 1. Popular
         * 2. Newest
         * 3. Oldest
         * 4. Cheapest
         */

        //public FrontPageVM SetupFPVM()
        //{
        //    FrontPageVM vModel = new();
        //    vModel.FrontPageMovieLists = new()
        //    {
        //        { FPMostPopular() },
        //        { FPNewest() },
        //        { FPOldest() },
        //        { FPCheapest() }
        //    };

        //    return vModel;
        //}

        #endregion


        #endregion

        #region Setups_And_UpdateVM()
        // UPDATE METHODS, Used for Setting and Updating
        public ArticleListVM UpdateVM()
        {
            ArticleListVM vModel = new();

            vModel.CategoryList = GetCategoryList();
            vModel.HeadlineList = GetHeadlineList();
            vModel.ArticleList = GetArticleList();

            return vModel;
        }

        public ArticleListVM UpdateVM(ArticleListVM vModel)
        {
            vModel.CategoryList = GetCategoryList();
            vModel.HeadlineList = GetHeadlineList();
            vModel.ArticleList = GetArticleList(vModel);

            return vModel;
        }

        public ArticleListVM UpdateVM(int pageNum, int pageSize)
        {
            ArticleListVM vModel = new();
            vModel.SelectedPageSize = Convert.ToString(pageSize);

            vModel.CategoryList = GetCategoryList();
            vModel.HeadlineList = GetHeadlineList();
            vModel.ArticleList = GetArticleList()
                .Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

            vModel.PageNum = pageNum;
            vModel.PageSize = pageSize;

            if (pageSize < GetAllArticles().Count())
            {
                vModel.MaxPage = (GetAllArticles().Count() % pageSize == 0)
                    ? GetAllArticles().Count() / pageSize : GetAllArticles().Count() / pageSize + 1;
            }
            else
                vModel.MaxPage = 1;

            return vModel;
        }

        public ArticleListVM UpdateVM(int pageNum, int pageSize, ArticleListVM vModel)
        {
            vModel.SelectedPageSize = Convert.ToString(pageSize);

            vModel.CategoryList = GetCategoryList();
            vModel.HeadlineList = GetHeadlineList();
            vModel.ArticleList = GetArticleList(vModel)
                .Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

            vModel.PageNum = pageNum;
            vModel.PageSize = pageSize;

            if (pageSize < GetAllArticles().Count())
            {
                vModel.MaxPage = (GetAllArticles().Count() % pageSize == 0)
                    ? GetAllArticles().Count() / pageSize : GetAllArticles().Count() / pageSize + 1;
            }
            else
                vModel.MaxPage = 1;

            return vModel;
        }

        #endregion
    }
}
