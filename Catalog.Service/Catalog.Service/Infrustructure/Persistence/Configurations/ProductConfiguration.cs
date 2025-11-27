using Catalog.Service.Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Service.Infrustructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");


            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedNever();

            builder.Property(p => p.Price).IsRequired().HasPrecision(6,2);

            builder.Property(p =>p.Name).IsRequired().IsUnicode().HasMaxLength(10);
            
            builder.Property(p=>p.Description).IsRequired(false).IsUnicode().HasMaxLength(15);  




        }
    }
}
