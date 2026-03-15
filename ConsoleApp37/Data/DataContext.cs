using ConsoleApp37.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp37.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;
                  Initial Catalog=CategoryAppDb;
                  Integrated Security=True");
        }
    }
}