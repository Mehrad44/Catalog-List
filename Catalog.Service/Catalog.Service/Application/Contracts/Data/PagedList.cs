namespace Catalog.Service.Application.Contracts.Data
{
    public class PagedList<TEntity>
    {
        private List<TEntity> _itmes = new();

        public IReadOnlyList<TEntity> Items => _itmes.AsReadOnly();  

        public int TotalRecordCount { get; init; }

        public PagedList(IEnumerable<TEntity> items , int totalRecordCount)
        {
            _itmes.AddRange(items);

            TotalRecordCount = totalRecordCount;    
        }
    }
}
