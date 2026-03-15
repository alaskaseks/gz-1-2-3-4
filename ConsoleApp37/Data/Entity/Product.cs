using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp37.Data.Entity
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Category? Category { get; set; }
        public Brand? Brand { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} | {Price}$ | Brand: {Brand?.Name ?? "no brand"} | {Description ?? "no description"}";
        }
    }
}