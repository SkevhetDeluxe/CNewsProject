using CNewsProject.Models.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CNewsProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<Article> Article { get; set; }
    }
}
