using System.Linq.Expressions;

namespace Catalog.Service.Domain.Primitives.Contracts
{
    public interface IRepository<TEntity, TId>
        where TEntity : IEntity<TId>
        where TId : notnull 
    {
        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);    


        Task DeleteAsync(TEntity entity);

        Task<TEntity?> GetByIdAsync(TId id);

        Task<IEnumerable<TEntity>>  FindEntitiesAsync(Expression<Func<TEntity,bool>> predicate);

        Task <(IEnumerable<TEntity>Entities , int TotalRecordCount)> FilterAsunc(string criteria,string? sort , int pageSize ,int pageIndex );

        Task<(IEnumerable<TEntity> Entities, int TotalRecordCount)> SearchAsunc(string text, string? sort, int pageSize, int pageIndex);

    }
}
