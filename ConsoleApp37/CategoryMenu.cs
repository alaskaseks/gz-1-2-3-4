using ConsoleApp37.Data;
using ConsoleApp37.Data.Entity;
using ConsoleApp37.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp37
{
    public class CategoryMenu(DataContext db)
    {
        private readonly DataContext _db = db;

        private Guid? ReadGuid()
        {
            return Guid.TryParse(Console.ReadLine(), out Guid id) ? id : null;
        }

        public void Run()
        {
            bool isExit = false;
            while (!isExit)
            {
                Console.Clear();

                Console.WriteLine("     category manager");
                Console.WriteLine(" 1.  get all categories");
                Console.WriteLine(" 2.  add category");
                Console.WriteLine(" 3.  delete category");
                Console.WriteLine(" 4.  find by name");
                Console.WriteLine(" 5.  find by id");
                Console.WriteLine(" 6.  update category");
                Console.WriteLine(" 7.  add brand");
                Console.WriteLine(" 8.  get all brands");
                Console.WriteLine(" 9.  add product");
                Console.WriteLine(" 10. update product");
                Console.WriteLine(" 11. soft delete product");
                Console.WriteLine(" 12. hard delete product");
                Console.WriteLine(" 13. product detailed info");
                Console.WriteLine(" 14. deleted products by category");
                Console.WriteLine(" 15. product count by category");
                Console.WriteLine(" 16. cheapest product by category");
                Console.WriteLine(" 17. average price by category");
                Console.WriteLine(" 18. product count by brand");
                Console.WriteLine(" 19. products sorted by brand");
                Console.WriteLine("   0.  exit");
                Console.Write("choice: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1": GetAll(); break;
                    case "2": Add(); break;
                    case "3": Delete(); break;
                    case "4": FindByName(); break;
                    case "5": FindById(); break;
                    case "6": Update(); break;
                    case "7": AddBrand(); break;
                    case "8": GetAllBrands(); break;
                    case "9": AddProduct(); break;
                    case "10": UpdateProduct(); break;
                    case "11": SoftDeleteProduct(); break;
                    case "12": HardDeleteProducts(); break;
                    case "13": ShowProductDetailed(); break;
                    case "14": ShowDeletedProducts(); break;
                    case "15": ShowProductCount(); break;
                    case "16": ShowCheapestProduct(); break;
                    case "17": ShowAveragePrice(); break;
                    case "18": ShowProductCountByBrand(); break;
                    case "19": ShowProductsSortedByBrand(); break;
                    case "0": isExit = true; break;
                    default:
                        Console.WriteLine("invalid choice");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void GetAll()
        {
            Console.Clear();
            var list = _db.Categories.Where(c => c.DeletedAt == null).ToList();
            if (list.Count == 0) Console.WriteLine("no categories found");
            else list.ForEach(c => Console.WriteLine(c));
            Console.ReadKey();
        }

        private void Add()
        {
            Console.Clear();
            Console.Write("name: "); var name = Console.ReadLine() ?? "";
            Console.Write("description: "); var desc = Console.ReadLine();
            _db.Categories.Add(new Category
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = string.IsNullOrWhiteSpace(desc) ? null : desc,
                CreatedAt = DateTime.Now
            });
            _db.SaveChanges();
            Console.WriteLine("category added");
            Console.ReadKey();
        }

        private void Delete()
        {
            Console.Clear();
            Console.Write("id: ");
            var id = ReadGuid();
            if (id == null) { Console.WriteLine("invalid id"); Console.ReadKey(); return; }
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) { Console.WriteLine("not found"); Console.ReadKey(); return; }
            category.DeletedAt = DateTime.Now;
            _db.SaveChanges();
            Console.WriteLine("deleted");
            Console.ReadKey();
        }

        private void FindByName()
        {
            Console.Clear();
            Console.Write("name: ");
            var name = Console.ReadLine() ?? "";
            var list = _db.Categories.Where(c => c.DeletedAt == null && c.Name.Contains(name)).ToList();
            if (list.Count == 0) Console.WriteLine("not found");
            else list.ForEach(c => Console.WriteLine(c));
            Console.ReadKey();
        }

        private void FindById()
        {
            Console.Clear();
            Console.Write("id: ");
            var id = ReadGuid();
            if (id == null) { Console.WriteLine("invalid id"); Console.ReadKey(); return; }
            var category = _db.Categories.FirstOrDefault(c => c.Id == id && c.DeletedAt == null);
            Console.WriteLine(category != null ? category : "not found");
            Console.ReadKey();
        }

        private void Update()
        {
            Console.Clear();
            Console.Write("id: ");
            var id = ReadGuid();
            if (id == null) { Console.WriteLine("invalid id"); Console.ReadKey(); return; }
            var category = _db.Categories.FirstOrDefault(c => c.Id == id && c.DeletedAt == null);
            if (category == null) { Console.WriteLine("not found"); Console.ReadKey(); return; }
            Console.Write($"new name ({category.Name}): "); var newName = Console.ReadLine();
            Console.Write($"new desc ({category.Description}): "); var newDesc = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) category.Name = newName;
            if (!string.IsNullOrWhiteSpace(newDesc)) category.Description = newDesc;
            _db.SaveChanges();
            Console.WriteLine("updated");
            Console.ReadKey();
        }

        private void AddBrand()
        {
            Console.Clear();
            Console.Write("brand name: "); var name = Console.ReadLine() ?? "";
            Console.Write("country: "); var country = Console.ReadLine();
            _db.Brands.Add(new Brand
            {
                Id = Guid.NewGuid(),
                Name = name,
                Country = string.IsNullOrWhiteSpace(country) ? null : country,
                CreatedAt = DateTime.Now
            });
            _db.SaveChanges();
            Console.WriteLine("brand added");
            Console.ReadKey();
        }

        private void GetAllBrands()
        {
            Console.Clear();
            var list = _db.Brands.Where(b => b.DeletedAt == null).ToList();
            if (list.Count == 0) Console.WriteLine("no brands found");
            else list.ForEach(b => Console.WriteLine(b));
            Console.ReadKey();
        }

        private void AddProduct()
        {
            Console.Clear();
            Console.Write("product name: "); var name = Console.ReadLine() ?? "";
            Console.Write("description: "); var desc = Console.ReadLine();
            Console.Write("price: "); double.TryParse(Console.ReadLine(), out double price);
            Console.Write("category id: "); var categoryId = ReadGuid();
            Console.Write("brand id: "); var brandId = ReadGuid();
            _db.Products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = string.IsNullOrWhiteSpace(desc) ? null : desc,
                Price = price,
                CategoryId = categoryId,
                BrandId = brandId,
                CreatedAt = DateTime.Now
            });
            _db.SaveChanges();
            Console.WriteLine("product added");
            Console.ReadKey();
        }

        private void UpdateProduct()
        {
            Console.Clear();
            Console.Write("product id: ");
            var id = ReadGuid();
            if (id == null) { Console.WriteLine("invalid id"); Console.ReadKey(); return; }
            var product = _db.Products.FirstOrDefault(p => p.Id == id && p.DeletedAt == null);
            if (product == null) { Console.WriteLine("not found"); Console.ReadKey(); return; }
            Console.Write($"new name ({product.Name}): "); var newName = Console.ReadLine();
            Console.Write($"new desc ({product.Description}): "); var newDesc = Console.ReadLine();
            Console.Write($"new price ({product.Price}): "); var newPrice = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) product.Name = newName;
            if (!string.IsNullOrWhiteSpace(newDesc)) product.Description = newDesc;
            if (double.TryParse(newPrice, out double price)) product.Price = price;
            _db.SaveChanges();
            Console.WriteLine("updated");
            Console.ReadKey();
        }

        private void SoftDeleteProduct()
        {
            Console.Clear();
            Console.Write("product id: ");
            var id = ReadGuid();
            if (id == null) { Console.WriteLine("invalid id"); Console.ReadKey(); return; }
            var product = _db.Products.FirstOrDefault(p => p.Id == id && p.DeletedAt == null);
            if (product == null) { Console.WriteLine("not found"); Console.ReadKey(); return; }
            product.DeletedAt = DateTime.Now;
            _db.SaveChanges();
            Console.WriteLine("soft deleted");
            Console.ReadKey();
        }

        private void HardDeleteProducts()
        {
            Console.Clear();
            var deleted = _db.Products.Where(p => p.DeletedAt != null).ToList();
            if (deleted.Count == 0) { Console.WriteLine("no soft-deleted products found"); Console.ReadKey(); return; }
            _db.Products.RemoveRange(deleted);
            _db.SaveChanges();
            Console.WriteLine($"hard deleted {deleted.Count} products");
            Console.ReadKey();
        }

        private void ShowProductDetailed()
        {
            Console.Clear();
            var products = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.DeletedAt == null)
                .Select(p => new ProductCardModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryName = p.Category != null ? p.Category.Name : "no category",
                    BrandName = p.Brand != null ? p.Brand.Name : "no brand"
                }).ToList();

            if (products.Count == 0) Console.WriteLine("no products");
            else products.ForEach(p => Console.WriteLine(p));
            Console.ReadKey();
        }

        private void ShowDeletedProducts()
        {
            Console.Clear();
            var categories = _db.Categories
                .Include(c => c.Products)
                .Where(c => c.DeletedAt == null)
                .ToList();

            foreach (var cat in categories)
            {
                var deleted = cat.Products?.Where(p => p.DeletedAt != null).ToList();
                Console.WriteLine($"\n[{cat.Name}]");
                if (deleted == null || deleted.Count == 0) Console.WriteLine("  no deleted products");
                else deleted.ForEach(p => Console.WriteLine($"  {p}"));
            }
            Console.ReadKey();
        }

        private void ShowProductCount()
        {
            Console.Clear();
            var result = _db.Categories
                .Where(c => c.DeletedAt == null)
                .Select(c => new
                {
                    c.Name,
                    Count = c.Products!.Count(p => p.DeletedAt == null)
                }).ToList();

            result.ForEach(r => Console.WriteLine($"{r.Name}: {r.Count} products"));
            Console.ReadKey();
        }

        private void ShowCheapestProduct()
        {
            Console.Clear();
            var categories = _db.Categories
                .Include(c => c.Products)
                .Where(c => c.DeletedAt == null)
                .ToList();

            foreach (var cat in categories)
            {
                var cheapest = cat.Products?.Where(p => p.DeletedAt == null).MinBy(p => p.Price);
                Console.WriteLine($"\n[{cat.Name}]");
                Console.WriteLine(cheapest != null ? $"  {cheapest}" : "  no products");
            }
            Console.ReadKey();
        }

        private void ShowAveragePrice()
        {
            Console.Clear();
            var result = _db.Categories
                .Where(c => c.DeletedAt == null)
                .Select(c => new
                {
                    c.Name,
                    Avg = c.Products!.Where(p => p.DeletedAt == null).Average(p => (double?)p.Price) ?? 0
                }).ToList();

            result.ForEach(r => Console.WriteLine($"{r.Name}: avg price = {r.Avg:F2}$"));
            Console.ReadKey();
        }

        private void ShowProductCountByBrand()
        {
            Console.Clear();
            var result = _db.Brands
                .Where(b => b.DeletedAt == null)
                .Select(b => new
                {
                    b.Name,
                    Count = b.Products!.Count(p => p.DeletedAt == null)
                }).ToList();

            result.ForEach(r => Console.WriteLine($"{r.Name}: {r.Count} products"));
            Console.ReadKey();
        }

        private void ShowProductsSortedByBrand()
        {
            Console.Clear();
            var products = _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.DeletedAt == null)
                .OrderBy(p => p.Brand != null ? p.Brand.Name : "zzz")
                .Select(p => new ProductCardModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryName = p.Category != null ? p.Category.Name : "no category",
                    BrandName = p.Brand != null ? p.Brand.Name : "no brand"
                }).ToList();

            if (products.Count == 0) Console.WriteLine("no products");
            else products.ForEach(p => Console.WriteLine(p));
            Console.ReadKey();
        }
    }
}
