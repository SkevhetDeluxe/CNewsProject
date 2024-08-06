using Microsoft.AspNetCore.Mvc.Rendering;
using CNewsProject.Models.DataBase;
using CNewsProject.Models.ViewModels;

namespace CNewsProject.Service
{
    public interface IArticleService
    {
        public string UploadBlob(IFormFile articleImage, string fileName);

        public List<Article> GetAllArticles();
        public List<Article> GetAllPublished();
        public Article GetArticleById(int Id);
        public FrontPageArticlesVM GetFrontPageArticleVM();
        public List<Article> GetPendingArticles();
        public void WriteArticle(WriteArticleVM newArticle, string authorName);
        public void RemoveArticle(Article article);
        public void EditArticle(Article article);
        public void UpdateArticle(Article article);
        public void PublishArticle(int id, string publisherName);
        public List<Article> SearchForArticles(string search, string category);
        public List<Article> GetArticleListByCategoryStringified(string category);

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

        #endregion
    }
}

