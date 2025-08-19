using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Domain.Entities
{
    public class ProductVariant
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = null!;
        public decimal Price { get; private set; }
        public string? Photo { get; private set; }
        public Guid ProductId { get; private set; }

        private ProductVariant() { }
        public ProductVariant(string name, decimal price, Guid productId, string? photo = null)
        {
            Name = name; Price = price; ProductId = productId; Photo = photo;
        }
    }
}
