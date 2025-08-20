using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = null!;
        public decimal Price { get; private set; }
        public Guid UserId { get; private set; }
        public User? User { get; private set; }


        private Product() { }
        public Product(string name, decimal price, Guid userId)
        {
            Name = name; Price = price; UserId = userId;
        }

        public void UpdatePrice(decimal price) => Price = price;
    }
}
