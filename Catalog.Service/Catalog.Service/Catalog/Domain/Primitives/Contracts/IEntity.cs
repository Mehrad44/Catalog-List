namespace Catalog.Service.Catalog.Domain.Primitives.Contracts
{
    public interface IEntity<TId>

    {
        TId Id { get;  }
    }
}
