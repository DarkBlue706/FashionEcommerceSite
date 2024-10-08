using Microsoft.EntityFrameworkCore;

namespace ShirtCompany.Models
{
    public class UserDBContext : DbContext
    {
        public DbSet<UserModel> Users {get; set;}

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
