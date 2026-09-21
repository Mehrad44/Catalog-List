using Catalog.Service.Catalog.Domain.Product;
using Catalog.Service.Catalog.Infrustructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Catalog.Infrustructure.Persistence
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) 
        {
             
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration( new ProductConfiguration());
            modelBuilder.HasDefaultSchema("Catalog");
        }
        public DbSet<Product> products => Set<Product>();

         
    }
}
