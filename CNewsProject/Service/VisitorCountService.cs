using CNewsProject.Data;
using CNewsProject.Models.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CNewsProject.Service
{
    public class VisitorCountService: IVisitorCountService
    {
        private readonly ApplicationDbContext _context;

            public VisitorCountService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task IncrementVisitorCountAsync(string pageName)
            {
                var visitorCount = await _context.VisitorCounts.FirstOrDefaultAsync(v => v.PageName == pageName);
                if (visitorCount == null)
                {
                    visitorCount = new VisitorCount { PageName = pageName, VisitCount = 1 };
                    _context.VisitorCounts.Add(visitorCount);
                }
                else
                {
                    visitorCount.VisitCount++;
                    _context.VisitorCounts.Update(visitorCount);
                }
                await _context.SaveChangesAsync();
            }

            public async Task<int> GetVisitorCountAsync(string pageName)
            {
                var visitorCount = await _context.VisitorCounts.FirstOrDefaultAsync(v => v.PageName == pageName);
                return visitorCount?.VisitCount ?? 0;
            }        
    }
}
