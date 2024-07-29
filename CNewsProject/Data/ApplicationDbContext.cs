using CNewsProject.Models.DataBase;
using CNewsProject.Models.DataBase.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CNewsProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Article> Article { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Subscription> Subscription { get; set; }
        
    }
}
