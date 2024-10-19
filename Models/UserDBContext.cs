using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace ShirtCompany.Models
{
public class UserDBContext : IdentityDbContext<IdentityUser>
{
    public DbSet<User> UserProfiles { get; set; }  // Renamed to avoid conflict

    public UserDBContext(DbContextOptions<UserDBContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=C:/Temp/ShirtCompanyDatabase.db");
        }
    }
}

}

