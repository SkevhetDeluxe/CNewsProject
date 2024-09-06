using CNewsProject.Models.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CNewsProject.Data
{
    public class FunctionDbContext(DbContextOptions<FunctionDbContext> options) : DbContext(options)
    {
        public DbSet<Article> Article { get; set; }
    }
}
