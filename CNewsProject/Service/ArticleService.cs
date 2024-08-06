using Microsoft.AspNetCore.Mvc.Rendering;
using CNewsProject.Models.DataBase;
using CNewsProject.Data;
using CNewsProject.Models.ViewModels;
using Azure.Storage.Blobs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Drawing;

namespace CNewsProject.Service
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _db;
		private readonly BlobServiceClient _blobServiceClient;
		private readonly IConfiguration _configuration;
		private readonly ICategoryService _categoryService;
		private readonly UserManager<AppUser> _userManager;

		public ArticleService(ApplicationDbContext db, IConfiguration configuration, ICategoryService cgs,
            UserManager<AppUser> userManager)
        {
            _db = db;
			_blobServiceClient = new BlobServiceClient(configuration["AzureBlobStorage"]);
			_configuration = configuration;
			_categoryService = cgs;
			_userManager = userManager;
		}

		//Blob UPLOADING()
        #region Blobl_Uploading()

		public string UploadBlob(IFormFile articleImage, string newFileName)
		{
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

        #endregion

        #region Base_Methods()
        public List<Article> GetAllArticles()
        {
            return _db.Article.OrderBy(a => a.Headline).ToList();
        }

		public List<Article> GetAllPublished()
		{
			return _db.Article.Where(a => a.Status == "Approved").OrderByDescending(a => a.PublishedDate).ToList();
		}

		public FrontPageArticlesVM GetFrontPageArticleVM()
		{
			if (_db.Article.Any())
				return new FrontPageArticlesVM()
				{
					MainArticle = _db.Article.OrderByDescending(a => a.PublishedDate).FirstOrDefault(),
					NotMainButStillImportantArticles = new(),
					TheRestLol = new()
				};
			else
				return new FrontPageArticlesVM();
		}

        public Article GetArticleById(int Id)
        {
            return _db.Article.Include(c => c.Category).FirstOrDefault(a => a.Id == Id)!;
        }

        public void WriteArticle(WriteArticleVM newArticle, string authorName)
        {
			Random rnd = new();
			string imgName = "articleimg" + Convert.ToString(DateTime.Now) + Convert.ToString(rnd.Next(2, int.MaxValue));
			Article article = new()
			{
				Headline = newArticle.Headline,
				Content = newArticle.Content,
				ContentSummary = newArticle.ContentSummary,
				LinkText = newArticle.Headline,
				ImageLink = UploadBlob(newArticle.ArticleImage, imgName),
				AuthorUserName = authorName
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
        }

		public void UpdateArticle(Article article)
		{
			foreach(PropertyInfo property in article.GetType().GetProperties().Where(p => p.Name != "Id"))
			{
                property.SetValue(GetArticleById(article.Id), property.GetValue(article));
            }
			_db.SaveChanges();
		}

		public void PublishArticle(int id, string publisherName)
		{
			GetArticleById(id).Status = "Approved";
			GetArticleById(id).ThePublisherUserName = publisherName;
			GetArticleById(id).PublishedDate = DateTime.Now;
			_db.SaveChanges();
		}

        public void RemoveArticle(Article article)
        {
            _db.Article.Remove(_db.Article.FirstOrDefault(a => a.Id == article.Id)!);
            _db.SaveChanges();
        }




        public void EditArticle (Article article)
        {
            GetArticleById(article.Id).PublishedDate= article.PublishedDate;
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

        #endregion

        #region Get_Lists_With_Filters_ThisNameOfTheRegionIsOldFromAnotherProject.NewName="SearchFunctionality"()
        // Overload later to take filters and sortings
        public List<Article> GetArticleListByCategory(Category category)
        {
            List<Article> articleList = _db.Article.Where(a => a.Category ==  category).ToList();

            return articleList;
        }

        public List<Article> GetArticleListByCategoryStringified(string category)
        {
			List<Article> articleList = _db.Article.Include(a => a.Category)
				.Where(a => a.Category.Name == category && a.Status == "Approved")
				.OrderByDescending(a => a.PublishedDate)
				.ToList();

            return articleList;
        }

        public List<Article> SearchForArticles(string search, string category)
		{
			List<string> exactSearch = new();
            if(search != null)
			{
				while (search.Contains("\""))
				{
					int pos1 = search.IndexOf("\"");
					search = search.Remove(pos1, 1);

					if (search.Contains("\"") == false)
						break;

					int pos2 = search.IndexOf("\"");
					search = search.Remove(pos2, 1);
					int length = pos2 - pos1;
					exactSearch.Add(search.Substring(pos1, length));
					search = search.Remove(pos1, length);
				}

				char[] delims = { ',', ' ', '.', '/' };
				List<string> searchSplit = new(search.Trim().ToLower().Split(delims));
				List<Article> searchResults = new();

				if (category == null)
				{
					searchResults = _db.Article.Where(a => a.Headline.ToLower().Contains(exactSearch[0]) || a.Content.ToLower().Contains(exactSearch[0]) && a.Headline.ToLower().Contains(search) || a.Content.ToLower().Contains(search)).ToList();
				}
				else
				{
					searchResults = _db.Article.Where(a => a.Category.Name == category && (a.Headline.ToLower().Contains(exactSearch[0]) || a.Content.ToLower().Contains(exactSearch[0])) && a.Headline.ToLower().Contains(search) || a.Content.ToLower().Contains(search)).ToList();
				}
				return searchResults;
			}

			return null;
		}

        #endregion
    }
}
