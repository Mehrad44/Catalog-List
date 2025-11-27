//using Bogus;
//using Catalog.Service.Domain.Product;

//namespace Catalog.Service.Infrustructure.Persistence.Repositories
//{
//    public class ProductFakeRepository : IProductReposioty
//    {
//        private List<Product> storage;

//        public ProductFakeRepository()
//        {
//            var faker = GetProductGenerator();
//            storage = faker.Generate(5);
//        }

//        public void Add(Product entity)
//        {
//           storage.Add(entity); 
//        }

//        public void Delete(Product entity)
//        {
//            throw new NotImplementedException();
//        }

//        public Product GetById(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Product> GetEntities()
//        {
//            return storage;
//        }

//        public void Update(Product entity)
//        {
//            throw new NotImplementedException();
//        }

//        private Faker<Product> GetProductGenerator()
//        {
//            var faker = new Faker<Product>()

//                .RuleFor(product => product.Name , f => f.Commerce.ProductName())
//                .RuleFor(product => product.Price , f => f.Random.Number(10, 100))
//                .RuleFor(product => product.Description , f => f.Commerce.ProductDescription());

//            return faker;
//        }
//    }
//}
