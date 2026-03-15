using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp37.Data.Entity
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} | {Description ?? "no description"} | Created: {CreatedAt:dd.MM.yyyy}";
        }
        public List<Product>? Products { get; set; }
    }
}