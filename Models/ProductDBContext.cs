using Microsoft.EntityFrameworkCore;

namespace ShirtCompany.Models
{
    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Product {get; set;}
        //public DbSet<Category> Categories { get; set; }

        public ProductDBContext(DbContextOptions<ProductDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)

            => options.UseSqlite("Data Source=C:/Temp/ShirtCompanyDatabase.db");
        
    }
}