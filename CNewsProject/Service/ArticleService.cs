using Microsoft.AspNetCore.Mvc.Rendering;
using CNewsProject.Models.DataBase;
using CNewsProject.Data;
using CNewsProject.Models.ViewModels;
using Azure.Storage.Blobs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Drawing;
using System.Security.Claims;
using Azure;
using MailKit.Search;

//using static System.Net.Mime.MediaTypeNames;

namespace CNewsProject.Service
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _db;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ArticleService(ApplicationDbContext db, IConfiguration configuration, ICategoryService cgs,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _blobServiceClient = new BlobServiceClient(configuration["AzureBlobStorage"]);
            _configuration = configuration;
            _categoryService = cgs;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //Blob UPLOADING()

        #region Blobl_Uploading()

        public string UploadBlob(IFormFile articleImage, string newFileName)
        {
            newFileName = newFileName.Replace("/", "-") + ".jpg";

            BlobContainerClient containerClient = _blobServiceClient
                .GetBlobContainerClient("images");

            BlobClient blobClient = containerClient.GetBlobClient(newFileName);

            using (var stream = articleImage.OpenReadStream())
            {
                blobClient.Upload(stream);
            }

            //Image img = Image.FromStream(articleImage.OpenReadStream(), true, true);
            //var newImg = new Bitmap()

            return blobClient.Uri.AbsoluteUri;
        }

        public bool DeleteBlob(string blobName)
        {
            try
            {
                var containers = _configuration.GetSection("imgContainers");

                List<bool> failList = new();
                foreach (var child in containers.GetChildren())
                {
                    BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(child.Value);

                    BlobClient blobClient = containerClient.GetBlobClient(blobName);

                    var result = blobClient.DeleteIfExists();

                    if (!result)
                        failList.Add(false);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        //Views and Likes

        #region Statistics()

        public void IncreaseViews(int id)
        {
            GetArticleById(id).Views++;
            _db.SaveChanges();
        }

        public void Laikalaininen(int id, string userId)
        {
            if (!HasLiked(id, userId))
            {
                GetArticleById(id).Likes++;
                _db.Users.Single(u => u.Id == userId).LikedArticles.Add(id);
                _db.SaveChanges();
            }
            else
            {
                GetArticleById(id).Likes--;
                _db.Users.Single(u => u.Id == userId).LikedArticles.Remove(id);
                _db.SaveChanges();
            }
        }


        public void Laikalaininen(int id, ClaimsPrincipal principal)
        {
            string userId = _userManager.GetUserAsync(principal).Result.Id;

            if (!HasLiked(id, userId))
            {
                GetArticleById(id).Likes++;
                _db.Users.Single(u => u.Id == userId).LikedArticles.Add(id);
                _db.SaveChanges();
            }
            else
            {
                GetArticleById(id).Likes--;
                _db.Users.Single(u => u.Id == userId).LikedArticles.Remove(id);
                _db.SaveChanges();
            }
        }

        private bool HasLiked(int id, string userId)
        {
            return _db.Users.Single(u => u.Id == userId).LikedArticles.Contains(id);
        }

        #endregion


        #region Base_Methods()

        public void AddToEditorsChoice(int id)
        {
            EditorsChoice selectedArticle = new() { Article = GetArticleById(id) };
            _db.EditorsChoice.Add(selectedArticle);
            _db.SaveChanges();
        }

        public List<Article> GetLatestArticles()
        {
            return _db.Article.OrderByDescending(a => a.PublishedDate).Take(5).ToList();
        }

        public List<Article> GetFiveArticles()
        {
            return _db.Article.OrderByDescending(a => a.Views).Take(5).ToList();
        }

        public List<Article> GetAllArticles()
        {
            return _db.Article.OrderBy(a => a.Headline).ToList();
        }

        public List<Article> GetAllPublished()
        {
            return _db.Article.Include(a => a.Category).Where(a => a.Status == "Approved")
                .OrderByDescending(a => a.PublishedDate).ToList();
        }

        public FrontPageArticlesVM GetFrontPageArticleVM()
        {
            if (_db.Article.Any())
            {
                var articles = _db.Article.Where(a => a.Status == "Approved").OrderByDescending(a => a.PublishedDate)
                    .AsNoTracking();
                return new FrontPageArticlesVM()
                {
                    MainArticle = articles.First(),
                    NotMainButStillImportantArticles = articles.Skip(1).ToList(),
                    TheRestLol = new()
                };
            }

            else
                return new FrontPageArticlesVM();
        }

        public CategoryPageArticlesVM GetCategoryPageArticleVM(string category)
        {
            List<Article> categoryArticles = GetArticleListByCategoryStringified(category, 0);
            if (categoryArticles.Any())
            {
                return new CategoryPageArticlesVM()
                {
                    MainArticle = categoryArticles.OrderByDescending(a => a.PublishedDate).FirstOrDefault()!,
                    NotMainButStillImportantArticles =
                        categoryArticles.OrderByDescending(a => a.PublishedDate).Skip(1).ToList(),
                    TheRestLol = new()
                };
            }

            else
                return new CategoryPageArticlesVM();
        }

        public Article GetArticleById(int Id)
        {
            return _db.Article.Include(c => c.Category).FirstOrDefault(a => a.Id == Id)!;
        }


        public void WriteArticle(WriteArticleVM newArticle, string content, string authorName, bool draft)
        {
            Article article = new()
            {
                Headline = newArticle.Headline,
                Content = content,
                ContentSummary = newArticle.ContentSummary,
                LinkText = newArticle.Headline,
                AuthorUserName = authorName,
                Status = (draft ? "Draft" : "Pending")
            };

            if (_categoryService.CategoryExists(newArticle.CategoryName))
            {
                article.Category = _categoryService.GetCategoryByName(newArticle.CategoryName);
                article.CategoryId = _categoryService.GetCategoryByName(newArticle.CategoryName).Id;
            }
            else
            {
                _categoryService.AddCategory(newArticle.CategoryName);
                article.Category = _categoryService.GetCategoryByName(newArticle.CategoryName);
                article.CategoryId = _categoryService.GetCategoryByName(newArticle.CategoryName).Id;
            }

            _db.Article.Add(article);
            _db.SaveChanges();

            // Now UpBlob

            var recentArticle = _db.Article.Single(a => a.Id == article.Id);

            string imgName = "article" + Convert.ToString(recentArticle.Id) + "img";

            string imgUrl = UploadBlob(newArticle.ArticleImage, imgName);
            _db.Article.Single(a => a.Id == article.Id).ImageLink = imgUrl;
            _db.SaveChanges();
        }

        public void RemoveArticle(Article article)
        {
            _db.Article.Remove(_db.Article.FirstOrDefault(a => a.Id == article.Id)!);
            _db.SaveChanges();
        }


        public void EditArticle(Article article)
        {
            GetArticleById(article.Id).PublishedDate = article.PublishedDate;
            GetArticleById(article.Id).LinkText = article.LinkText;
            GetArticleById(article.Id).Headline = article.Headline;
            GetArticleById(article.Id).ContentSummary = article.ContentSummary;
            GetArticleById(article.Id).Content = article.Content;
            GetArticleById(article.Id).Views = article.Views;
            GetArticleById(article.Id).Likes = article.Likes;
            GetArticleById(article.Id).ImageLink = article.ImageLink;
            GetArticleById(article.Id).Category = article.Category;

            _db.SaveChanges();
        }

        public async Task<List<string>> GetAllAuthorNames()
        {
            var allUsers = _db.Users.ToList();
            List<string> authorNames = new();

            foreach (var user in allUsers)
            {
                if (await _userManager.IsInRoleAsync(user, "Journalist"))
                    authorNames.Add(user.UserName);
            }

            return authorNames;
        }

        #endregion

        //Fetch Pending, Approved and Declined Articles for Journalist. VIEEEEEEW COMPONENTO!!!

        #region Journalist_Fetching_Stuff

        public AuthorArticlesVM GetArticlesForAuthor(string authorUserName)
        {
            AuthorArticlesVM ArticleLists = new();

            List<Article> AllAuthorArticles = _db.Article.Where(a => a.AuthorUserName == authorUserName)
                .OrderByDescending(a => a.PublishedDate).ToList();

            ArticleLists.Pending = AllAuthorArticles.Where(a => a.Status == "Pending").ToList();
            ArticleLists.Approved = AllAuthorArticles.Where(a => a.Status == "Approved").ToList();
            ArticleLists.Declined = AllAuthorArticles.Where(a => a.Status == "Declined").ToList();

            return ArticleLists;
        }

        #endregion

        #region ThePublisher Related

        public List<Article> GetPendingArticles()
        {
            return _db.Article.Where(a => a.Status == "Pending").OrderBy(a => a.WrittenDate).ToList();
        }

        public void UpdateArticle(Article article)
        {
            foreach (PropertyInfo property in article.GetType().GetProperties().Where(p => p.Name != "Id"))
            {
                property.SetValue(GetArticleById(article.Id), property.GetValue(article));
            }

            _db.SaveChanges();
        }

        public bool UpdateArticleFromEditVM(EditArticleVM vModel, bool draft)
        {
            var article = GetArticleById(vModel.ArticleId);

            if (article == null)
                return false;

            if (vModel.ArticleImage != null)
            {
                string imgName = article.ImageLink.Replace("https://cnewsstorage.blob.core.windows.net/images/", "");

                var deleted = DeleteBlob(imgName);

                if (deleted)
                {
                    imgName = "article" + Convert.ToString(article.Id) + "img";
                    string newLink = UploadBlob(vModel.ArticleImage, imgName);
                    article.ImageLink = newLink;
                }
            }

            try
            {
                article.Category = _categoryService.GetCategoryByName(vModel.CategoryName);
                article.Headline = vModel.Headline;
                article.ContentSummary = vModel.ContentSummary;
                article.Content = vModel.Content;
                article.WrittenDate = DateTime.Now;
                article.Status = draft ? "Draft" : "Pending";
            }
            catch
            {
                return false;
            }

            _db.SaveChanges();
            return true;
        }

        public void PublishArticle(int id, string publisherName)
        {
            GetArticleById(id).Status = "Approved";
            GetArticleById(id).ThePublisherUserName = publisherName;
            GetArticleById(id).PublishedDate = DateTime.Now;
            _db.SaveChanges();
        }

        public void DeclineArticle(int id, string reason)
        {
            GetArticleById(id).Status = "Declined";
            //GetArticleById(id).DeclineMessage = reason;
            _db.SaveChanges();
        }

        #endregion

        #region Get_Lists_With_Filters_ThisNameOfTheRegionIsOldFromAnotherProject.NewName("SearchFunctionality")

        // Overload later to take filters and sortings

        public List<Article> GetArticleListByCategoryStringified(string category, int count)
        {
            if (count != 0)
            {
                //.Include(a => a.Category)
                return _db.Article
                    .Where(a => a.Category.Name == category && a.Status == "Approved")
                    .OrderByDescending(a => a.PublishedDate).Take(count)
                    .ToList();
            }

            //.Include(a => a.Category) ONLY needed if you want to mess with the CATEGORY
            return _db.Article
                .Where(a => a.Category.Name == category && a.Status == "Approved")
                .OrderByDescending(a => a.PublishedDate)
                .ToList();
        }

        internal class QueryLists
        {
            internal List<string> Exact { get; set; } = new();
            internal List<string> Split { get; set; } = new();
            internal List<string> Exclude { get; set; } = new();
        }

        internal QueryLists GenerateQueryLists(string query)
        {
            QueryLists qLists = new();

            while (query.Contains('\"'))
            {
                int pos1 = query.IndexOf('\"');
                query = query.Remove(pos1, 1);

                if (query.Contains('\"') == false)
                    break;

                int pos2 = query.IndexOf('\"');
                query = query.Remove(pos2, 1);
                int length = pos2 - pos1;
                qLists.Exact.Add(query.ToLower().Substring(pos1, length));
                query = query.Remove(pos1, length);
            }

            char[] delims = { ',', ' ', '.', '/' };
            qLists.Split = new(query.Trim().ToLower().Split(delims));

            for (int i = 0; i < qLists.Split.Count; i++)
            {
                if (qLists.Split[i].StartsWith("-"))
                {
                    qLists.Split.Add(qLists.Split[i].Remove(0, 1));
                    qLists.Split.RemoveAt(i);
                }
            }

            return qLists;
        }

        internal List<Article>? QuerySearch(string? query, List<Article> result)
        {
            if (!string.IsNullOrEmpty(query))
            {
                QueryLists qLists = GenerateQueryLists(query);

                if (qLists.Exact != null)
                    for (int i = 0; i < qLists.Exact.Count; i++)
                        result = result.Where(a =>
                            a.Headline.ToLower().Contains(qLists.Exact[i]) ||
                            a.Content.ToLower().Contains(qLists.Exact[i])).ToList();

                if (qLists.Split != null)
                    for (int i = 0; i < qLists.Split.Count; i++)
                        result = result.Where(a =>
                            a.Headline.ToLower().Contains(qLists.Split[i]) ||
                            a.Content.ToLower().Contains(qLists.Split[i])).ToList();

                if (qLists.Exclude != null)
                    result = result.Where(a => !qLists.Exclude.Any(h => a.Headline.ToLower().Contains(h))).ToList();
            }

            return result;
        }

        public SearchResult SearchForArticles(string? searchQuery, string? category)
        {
            SearchResult result = new();
            List<Article> articleList = new();

            if (category == null)
                articleList = GetAllPublished();

            else
                articleList = GetArticleListByCategoryStringified(category, 0); // 0 gives all

            result.Articles = QuerySearch(searchQuery, articleList);

            if (result.Articles != null)
                result.Succeeded = true;

            return result;
        }

        #endregion

        //Shhhhhhhhhhhhhhhhhhhhhhhhhh

        #region We Don't talk anbout this one

        public void GetTheRealStats()
        {
            Random rnd = new();
            foreach (var sak in _db.Article)
            {
                int temp = rnd.Next(11, 319);
                int temp2 = rnd.Next(45, 454);
                sak.Likes += temp;
                sak.Views += temp + temp2;
            }

            _db.SaveChanges();
        }

        #endregion
    }

    public class SearchResult
    {
        public bool Succeeded { get; set; }
        public List<Article>? Articles { get; set; }
    }
}