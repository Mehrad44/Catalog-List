using Catalog.Service.Catalog.Infrustructure.Persistence.Configurations;
using Catalog.Service.Security.Services.Domain.Roles;
using Catalog.Service.Security.Services.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Security.Services.Infrustrctre.Persistance
{
    public class SecuirityContext : IdentityDbContext<User, Role, Guid>
    {
        public SecuirityContext(DbContextOptions<SecuirityContext>options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.HasDefaultSchema("Catalog");
        }
    }
}
