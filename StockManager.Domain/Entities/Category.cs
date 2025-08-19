using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = null!;
        public Guid UserId { get; private set; }

        public ICollection<Product> Products { get; private set; } = new List<Product>();

        // For EF
        private Category() { }

        public Category(string name, Guid userId)
        {
            Name = name;
            UserId = userId;
        }

        public void Rename(string name) => Name = name;
    }
}
