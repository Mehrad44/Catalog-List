using Catalog.Service.Domain.Product;
using Catalog.Service.Infrustructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Infrustructure.Persistence
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) 
        {
             
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration( new ProductConfiguration());
        }
        public DbSet<Product> products => Set<Product>();

         
    }
}
