using System.Threading.Tasks;

namespace CNewsProject.Service
{
    public interface IVisitorCountService
    {
        Task IncrementVisitorCountAsync(string pageName);
        Task<int> GetVisitorCountAsync(string pageName);
    }
}
