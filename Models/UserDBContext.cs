using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ShirtCompany.Models
{
    public class UserDBContext : IdentityDbContext
    {
        public DbSet<UserModel>  Users {get; set;}

        public UserDBContext(DbContextOptions<UserDBContext> options)
            : base(options)
        {
        }

        public UserDBContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)

            => options.UseSqlite("Data Source=C:/Temp/ShirtCompanyDatabase.db");
        
    }
}
