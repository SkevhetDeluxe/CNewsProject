using CNewsFunctions.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CNewsFunctions.Data
{
    public class FunctionDbContext(DbContextOptions<FunctionDbContext> options) :IdentityDbContext<AppUser>(options)
    {
        public DbSet<Article> Article { get; set; }
    }
}
