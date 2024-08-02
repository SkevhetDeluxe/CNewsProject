using Microsoft.AspNetCore.Mvc.Rendering;
using CNewsProject.Models.DataBase;
using CNewsProject.Data;
using CNewsProject.Models.ViewModels;
using Azure.Storage.Blobs;

namespace CNewsProject.Service
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _db;
		private readonly BlobServiceClient _blobServiceClient;
		private readonly IConfiguration _configuration;
		private readonly ICategoryService _categoryService;

		public ArticleService(ApplicationDbContext db, IConfiguration configuration, ICategoryService cgs)
        {
            _db = db;
			_blobServiceClient = new BlobServiceClient(configuration["AzureBlobStorage"]);
			_configuration = configuration;
			_categoryService = cgs;
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

			return blobClient.Uri.AbsoluteUri;
		}

        #endregion

        #region Base_Methods()
        public List<Article> GetAllArticles()
        {
            return _db.Article.OrderBy(a => a.Headline).ToList();
        }

        public Article GetArticleById(int Id)
        {
            return _db.Article.FirstOrDefault(a => a.Id == Id)!;
        }

        public void WriteArticle(WriteArticleVM newArticle)
        {
			Random rnd = new();
			string imgName = "articleimg" + Convert.ToString(DateTime.Now) + Convert.ToString(rnd.Next(2, int.MaxValue));
			Article article = new()
			{
				Headline = newArticle.Headline,
				Content = newArticle.Content,
				ContentSummary = newArticle.ContentSummary,
				LinkText = newArticle.Headline,
				ImageLink = UploadBlob(newArticle.ArticleImage, imgName)
            };

			if (_categoryService.CategoryExists(newArticle.CategoryName))
				article.Category = _categoryService.GetCategoryByName(newArticle.CategoryName);
			else
			{
                _categoryService.AddCategory(newArticle.CategoryName);
				article.Category = _categoryService.GetCategoryByName(newArticle.CategoryName);
            }

            _db.Article.Add(article);
            _db.SaveChanges();
        }

        public void RemoveArticle(Article article)
        {
            _db.Article.Remove(_db.Article.FirstOrDefault(a => a.Id == article.Id)!);
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
        public List<Article> GetArticleListByCategory(Category category)
        {
            List<Article> articleList = _db.Article.Where(a => a.Category ==  category).ToList();

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

		#region Extra shit we cannot possibly not probably definetaly no need bruv. or?
		//public List<Article> GetArticleList(ArticleListVM vModel)
		//{
		//    List<Article> articleList = GetAllArticles();

		//    if (vModel.Headline != "All")
		//        articleList = articleList.Where(m => m.Headline == vModel.Headline).ToList();

		//    if (vModel.Category != "All")
		//        articleList = articleList.Where(m => m.Category == vModel.Category).ToList();

		//    return articleList;
		//}


		//// Get SORTINGS and FILTERS
		//public List<string> GetHeadlines() // Gets a List<string> of Headlines. No Dupes.
		//{
		//    List<string> headlines = new()
		//    {
		//        "All"
		//    };

		//    foreach (Article article in GetAllArticles())
		//    {
		//        if (!headlines.Contains(article.Headline))
		//            headlines.Add(article.Headline);
		//    }

		//    return headlines;
		//}

		//public List<string> GetCategories() // Gets a List<string> of Categories. No Dupes.
		//{
		//    List<string> categories = new()
		//    {
		//        "All"
		//    };

		//    foreach (Article article in GetAllArticles())
		//    {
		//        if (!categories.Contains(article.Category))
		//            categories.Add(article.Category);
		//    }

		//    return categories;
		//}

		//// SELECT LISTS
		//public List<SelectListItem> GetHeadlineList()
		//{
		//    List<SelectListItem> headlineList = new();

		//    foreach (string headline in GetHeadlines())
		//    {
		//        headlineList.Add(new SelectListItem
		//        {
		//            Value = headline,
		//            Text = headline
		//        });
		//    }

		//    return headlineList;
		//}

		//public List<SelectListItem> GetCategoryList()
		//{
		//    List<SelectListItem> categoryList = new();

		//    foreach (string category in GetCategories())
		//    {
		//        categoryList.Add(new SelectListItem
		//        {
		//            Value = category,
		//            Text = category
		//        });
		//    }

		//    return categoryList;
		//}
		#endregion
		#endregion


		#region Specifics_For_FrontPage()

		#region Extra shit we dont need. Probably
		//// These Methods are for displaying TOP Fives on the front page

		////public List<Article> FPMostPopular()
		////{
		////    var fpmostpopular = _db.OrderRows.GroupBy(or => or.ArticleId)
		////       .OrderByDescending(g => g.Count())
		////       .Select(m => new Article()
		////       {
		////           Id = m.Key,
		////           Headline = m.First().Article.Headline,
		////           Views = m.First().Article.Views,
		////           Likes = m.First().Article.Likes,

		////       }).Take(5).ToList();

		////    return fpmostpopular;
		////}


		//public List<Article> FPNewest()
  //      {
  //          List<Article> articleList = new();
  //          articleList = GetAllArticles().OrderByDescending(m => m.DateStamp)
  //              .Take(5).ToList();

  //          return articleList;
  //      }
  //      public List<Article> FPOldest()
  //      {
  //          List<Article> articleList = new();
  //      articleList = GetAllArticles().OrderBy(m => m.DateStamp)
  //              .Take(5).ToList();

  //          return articleList;
  //      }
		////public List<Article> FPCheapest()
		////{
		////    List<Article> articleList = new();
		////articleList = GetAllArticles().OrderBy(m => m.Price)
		////        .Take(5).ToList();

		////    return articleList;
		////}

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
		#endregion

		#region Setups_And_UpdateVM()

		#region Extra shit we wont need. Probably
		//// UPDATE METHODS, Used for Setting and Updating
		//public ArticleListVM UpdateVM()
  //      {
  //          ArticleListVM vModel = new();

  //          vModel.CategoryList = GetCategoryList();
  //          vModel.HeadlineList = GetHeadlineList();
  //          vModel.ArticleList = GetArticleList();

  //          return vModel;
  //      }

  //      public ArticleListVM UpdateVM(ArticleListVM vModel)
  //      {
  //          vModel.CategoryList = GetCategoryList();
  //          vModel.HeadlineList = GetHeadlineList();
  //          vModel.ArticleList = GetArticleList(vModel);

  //          return vModel;
  //      }

  //      public ArticleListVM UpdateVM(int pageNum, int pageSize)
  //      {
  //          ArticleListVM vModel = new();
  //          vModel.SelectedPageSize = Convert.ToString(pageSize);

  //          vModel.CategoryList = GetCategoryList();
  //          vModel.HeadlineList = GetHeadlineList();
  //          vModel.ArticleList = GetArticleList()
  //              .Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

  //          vModel.PageNum = pageNum;
  //          vModel.PageSize = pageSize;

  //          if (pageSize < GetAllArticles().Count())
  //          {
  //              vModel.MaxPage = (GetAllArticles().Count() % pageSize == 0)
  //                  ? GetAllArticles().Count() / pageSize : GetAllArticles().Count() / pageSize + 1;
  //          }
  //          else
  //              vModel.MaxPage = 1;

  //          return vModel;
  //      }

  //      public ArticleListVM UpdateVM(int pageNum, int pageSize, ArticleListVM vModel)
  //      {
  //          vModel.SelectedPageSize = Convert.ToString(pageSize);

  //          vModel.CategoryList = GetCategoryList();
  //          vModel.HeadlineList = GetHeadlineList();
  //          vModel.ArticleList = GetArticleList(vModel)
  //              .Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

  //          vModel.PageNum = pageNum;
  //          vModel.PageSize = pageSize;

  //          if (pageSize < GetAllArticles().Count())
  //          {
  //              vModel.MaxPage = (GetAllArticles().Count() % pageSize == 0)
  //                  ? GetAllArticles().Count() / pageSize : GetAllArticles().Count() / pageSize + 1;
  //          }
  //          else
  //              vModel.MaxPage = 1;

  //          return vModel;
  //      }

        #endregion
        #endregion
    }
}
