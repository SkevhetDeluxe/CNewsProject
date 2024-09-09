using Microsoft.AspNetCore.Mvc.Rendering;
using CNewsProject.Models.DataBase;
using CNewsProject.Models.ViewModels;
using System.Security.Claims;


namespace CNewsProject.Service
{
    public interface IArticleService
    {
        public void IncreaseViews(int id);
        public Article GetArticleById(int Id);
        public List<Article> GetAllArticles();
        public List<Article> GetAllPublished();
        public void EditArticle(Article article);
        public List<Article> GetPendingArticles();
        public void RemoveArticle(Article article);
        public void UpdateArticle(Article article);
        public Task<IList<string>> GetAllAuthorNames();
        public void Laikalaininen(int id, string userId);
        public void DeclineArticle(int id, string reason);
        public FrontPageArticlesVM GetFrontPageArticleVM();
        public SearchResult SearchForArticles(string search, string category);
        public void PublishArticle(int id, string publisherName);
        public void Laikalaininen(int id, ClaimsPrincipal principal);
        public string UploadBlob(IFormFile articleImage, string fileName);
        public bool UpdateArticleFromEditVM(EditArticleVM vModel, bool draft);

        public void WriteArticle(WriteArticleVM newArticle, string content, string authorName, bool draft);
        public CategoryPageArticlesVM GetCategoryPageArticleVM(string category);
        public List<Article> GetArticleListByCategoryStringified(string category, int count);


        //public List<SelectListItem> GetHeadlineList();
        //public List<SelectListItem> GetCategoryList();


        #region UpdateVM()_Overloads.
        //public ArticleListVM UpdateVM();
        //public ArticleListVM UpdateVM(ArticleListVM vModel);
        //public ArticleListVM UpdateVM(int id, int pageSize);
        //public ArticleListVM UpdateVM(int id, int pageSize, ArticleListVM vModel);
        #endregion


        #region Only_For_FrontPage

        //// Not sure if access is needed to these.
        //// Leaving them accessable for now.
        ////public List<Article> FPMostPopular();
        //public List<Article> FPNewest();
        //public List<Article> FPOldest();
        ////public List<Article> FPCheapest();

        ////public FrontPageVM SetupFPVM();



































































































































































































































        //Shhhhhhhhhhhhhhhhhhhh
        #region Shhhhhhhhhhhhhhhhhhh
        public void GetTheRealStats();

        #endregion
        #endregion
    }
}

