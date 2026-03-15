namespace ConsoleApp37.Data.Model
{
    public class ProductCardModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Name} | {Price}$ | Category: {CategoryName} | Brand: {BrandName} | {Description ?? "no description"}";
        }
    }
}