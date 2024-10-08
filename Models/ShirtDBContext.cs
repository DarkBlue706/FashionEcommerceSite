using Microsoft.EntityFrameworkCore;

namespace ShirtCompany.Models
{
    public class ShirtDBContext : DbContext
    {
        public DbSet<Shirt> Shirt {get; set;}

        public ShirtDBContext(DbContextOptions<ShirtDBContext> options)
            : base(options)
        {
        }

        public ShirtDBContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)

            => options.UseSqlite("Data Source=C:/Temp/ShirtCompanyDatabase.db");
        
    }
}