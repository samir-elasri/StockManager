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
        public string? Photo { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid UserId { get; private set; }

        public ICollection<ProductVariant> Variants { get; private set; } = new List<ProductVariant>();

        private Product() { }
        public Product(string name, decimal price, Guid categoryId, Guid userId, string? photo = null)
        {
            Name = name; Price = price; CategoryId = categoryId; UserId = userId; Photo = photo;
        }

        public void UpdatePrice(decimal price) => Price = price;
    }
}
