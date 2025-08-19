using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;

        // navigation
        public ICollection<Category> Categories { get; private set; } = new List<Category>();

        public User() { } // EF
        public User(string email, string passwordHash)
        {
            Id = Guid.NewGuid();
            Email = email;
            PasswordHash = passwordHash;
        }

        public void UpdatePassword(string newHash) => PasswordHash = newHash;
    }
}
