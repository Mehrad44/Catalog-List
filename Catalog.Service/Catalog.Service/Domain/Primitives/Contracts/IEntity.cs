namespace Catalog.Service.Domain.Primitives.Contracts
{
    public interface IEntity<TId>

    {
        TId Id { get;  }
    }
}
