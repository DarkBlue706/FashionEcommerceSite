using Microsoft.EntityFrameworkCore;

namespace ShirtCompany.Models
{
    public class CartItemDBContext : DbContext
    {
        public DbSet<CartItem> CartItem { get; set; }

        public CartItemDBContext(DbContextOptions<CartItemDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=C:/Temp/ShirtCompanyDatabase.db");
    }

}