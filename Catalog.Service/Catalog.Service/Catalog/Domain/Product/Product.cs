using Catalog.Service.Catalog.Domain.Primitives;

namespace Catalog.Service.Catalog.Domain.Product
{
    public class Product : Entity<Guid>
    {

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public string Description { get;  set; } 

        public Product(string name, decimal price, string description = "") : base(Guid.NewGuid())
        {
            Name = name;
            Price = price;
            Description = description;
        }

        public Product() : base(Guid.NewGuid()) 
        {
            
        }


        public void ChangePrice(int price)
        {
            Price = price;
        }

        public void ChangeName(string name)
        {
            Name = name;    
        }
    }
}
