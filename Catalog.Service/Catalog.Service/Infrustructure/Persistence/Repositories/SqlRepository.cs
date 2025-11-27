using Catalog.Service.Domain.Primitives;
using Catalog.Service.Domain.Primitives.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
namespace Catalog.Service.Infrustructure.Persistence.Repositories
{
    public class SqlRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TId : notnull
        where TEntity : Entity<TId>

    {
        private DbContext _context;
        protected DbSet<TEntity> set;


        public SqlRepository(CatalogContext context)
        {
            _context = context;
            set = context.Set<TEntity>();   
        }

        public async Task AddAsync(TEntity entity)
        {
          await  set.AddAsync(entity);    
          await _context.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(TEntity entity)
        {
            set.Remove(entity);
           await _context.SaveChangesAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TId id)
        {
         return  await set.FindAsync(id); 

        }

        public async Task<IEnumerable<TEntity>> FindEntitiesAsync(
       //? Compile
       Expression<Func<TEntity, bool>> predicate
   )
        {
            return await set.Where(predicate).ToListAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var entry = _context.Entry(entity); 
            entry.State = EntityState.Modified;
       
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<TEntity> Entities, int TotalRecordCount)> FilterAsunc(string? criteria , string? sort , int pageSize , int pageIndex)
        {
            var query = set.AsNoTracking();
            if(!string.IsNullOrEmpty(criteria))
            {
                query = query.Where(criteria);  
            }
            if (!string.IsNullOrEmpty(sort))
            {
                query = query.OrderBy(sort);
            }

            var entities =  await query.Skip((pageIndex -1) * pageSize).Take(pageSize).ToListAsync();

            var TotalRecordCount = await query.CountAsync();    
            return (entities,TotalRecordCount);
        }

        
        public virtual Task<(IEnumerable<TEntity> Entities, int TotalRecordCount)> SearchAsunc(string text, string? sort, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }
    }
}
