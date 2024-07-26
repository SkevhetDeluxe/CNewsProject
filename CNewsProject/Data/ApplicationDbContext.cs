using CNewsProject.Models.DataBase;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CNewsProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Article> Article { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Subscription> Subscription { get; set; }

        public DbSet<SubscriptionType> SubscriptionType { get; set; }

        

    }
}
