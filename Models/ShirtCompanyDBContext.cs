using Microsoft.EntityFrameworkCore;

namespace ShirtCompany.Models
{
    public class ShirtCompanyDBContext : DbContext 
    {
        public ShirtCompanyDBContext(DbContextOptions<ShirtCompanyDBContext> options) : base(options)
        {

        }
        public DbSet<Shirt> Shirts {get; set;}

    }

}