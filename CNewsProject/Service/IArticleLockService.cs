namespace CNewsProject.Service
{
    public interface IArticleLockService
    {
        bool IsArticleLocked(Models.DataBase.Article article);
    }
}
