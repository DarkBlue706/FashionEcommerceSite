using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShirtCompany.Models;
using ShirtCompany.Filters.ActionFilters;
using ShirtCompany.Filters.ExceptionFilters;
using ShirtCompany.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews(); 
builder.Services.AddRazorPages(); // Razor Pages need to be explicitly registered

// Configure the SQLite database connection
string? connectionString = builder.Configuration.GetConnectionString("ShirtCompanyDatabase");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'ShirtCompanyDatabase' is not found.");
}

// Register DbContexts
builder.Services.AddDbContext<UserDBContext>(options => 
    options.UseSqlite(connectionString));
builder.Services.AddDbContext<ProductDBContext>(options => 
    options.UseSqlite(connectionString));
builder.Services.AddDbContext<CartItemDBContext>(options => 
    options.UseSqlite(connectionString));

// Configure Identity with Entity Framework stores
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<UserDBContext>()
.AddDefaultTokenProviders();

// Add Cookie Authentication Middleware
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/Account/Login"; // Redirects to Login page
        options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // Access denied path
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie expiration
    });

// Register filters and services
builder.Services.AddScoped<Product_ValidateProductIdFilterAttribute>();
builder.Services.AddScoped<Product_HandleUpdateExceptionsFilterAttribute>();
builder.Services.AddScoped<Product_ValidateProductPriceFilter>();
builder.Services.AddScoped<CartService>();

// Add session services and HttpContextAccessor
builder.Services.AddSession(); // Adds session handling
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Enforce HSTS for production
}

app.UseHttpsRedirection(); // Ensure all traffic is HTTPS
app.UseStaticFiles(); // Serve static files like CSS/JS

app.UseSession(); // Register session middleware before routing
app.UseRouting(); // Set up routing

app.UseAuthentication(); // Register Authentication Middleware before Authorization
app.UseAuthorization();  // Register Authorization Middleware

// Map Razor Pages and default controller routes
app.MapRazorPages(); // Required for Identity UI pages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Run the application
