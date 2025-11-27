using System.Linq.Dynamic.Core;
using Catalog.Service.Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Infrustructure.Persistence.Repositories
{
    public class ProductSqlRepository : SqlRepository<Product , Guid> , IProductReposioty
    {

        public ProductSqlRepository(CatalogContext context) : base(context)

        {
            
        }

        public override async Task<(IEnumerable<Product> Entities, int TotalRecordCount)> SearchAsunc(string text, string? sort, int pageSize, int pageIndex)
        {
            var query = set.AsNoTracking();

            if (!string.IsNullOrEmpty(text))
            {
                query = query.Where(product => EF.Functions.Like(product.Name, $"%{text}%") || EF.Functions.Like(product.Description, $"{text}")) ;
            }
            if (!string.IsNullOrEmpty(sort))
            {
                query = query.OrderBy(sort);
            }

            var entities = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var TotalRecordCount = await query.CountAsync();
            return (entities, TotalRecordCount);
        }
    }
}
