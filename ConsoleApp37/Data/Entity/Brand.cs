using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp37.Data.Entity
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public List<Product>? Products { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} | {Country ?? "no country"}";
        }
    }
}