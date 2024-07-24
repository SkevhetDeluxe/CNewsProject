using CNewsProject.Models.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CNewsProject.Data
{
    public class CNewsDbContext : DbContext
    {
        public CNewsDbContext()
        {
        }

        public CNewsDbContext(DbContextOptions<CNewsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Employe> Employes { get; set; }

    }
}
