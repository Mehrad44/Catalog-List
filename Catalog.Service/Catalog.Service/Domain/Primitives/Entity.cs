using Catalog.Service.Domain.Primitives.Contracts;

namespace Catalog.Service.Domain.Primitives
{
    public abstract class Entity<TId> : IEntity<TId>
        where TId : notnull
    {
        public TId Id { get; }

        public Entity(TId id)
        {
            Id = id; 
        }
    }
}
