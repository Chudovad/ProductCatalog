using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.CategoryId);
            modelBuilder.Entity<Category>().HasData(new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Еда"
                },
                new Category
                {
                    Id = 2,
                    Name = "Вкусности"
                },
                new Category
                {
                    Id = 3,
                    Name = "Вода"
                }
            });
            modelBuilder.Entity<Product>().HasData(new List<Product>
            {
                new Product {
                    Id = 1,
                    Name = "Селедка",
                    Description = "Селедка соленая",
                    Price = 10,
                    GeneralNote = "Акция",
                    SpecialNote = "Пересоленая",
                    CategoryId = 1,
                },
                new Product {
                    Id = 2,
                    Name = "Тушенка",
                    Description = "Тушенка говяжья",
                    Price = 20,
                    GeneralNote = "Вкусная",
                    SpecialNote = "Жилы",
                    CategoryId = 1
                },
                new Product {
                    Id = 3,
                    Name = "Сгущенка",
                    Description = "В банках",
                    Price = 30,
                    GeneralNote = "С ключом",
                    SpecialNote = "Вкусная",
                    CategoryId = 2
                },
                new Product {
                    Id = 4,
                    Name = "Квас",
                    Description = "В бутылках",
                    Price = 15,
                    GeneralNote = "Вятский",
                    SpecialNote = "Теплый",
                    CategoryId= 3,
                }
            });
            base.OnModelCreating(modelBuilder);

        }
    }
}
