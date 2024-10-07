using Microsoft.EntityFrameworkCore;
using ShirtCompany.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the SQLite connection
builder.Services.AddDbContext<ShirtCompanyDBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ShirtCompanyDatabase")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseStaticFiles();

//app.UseAuthorization();      // Enables authentication/authorization middlewar
// API Routing

app.UseRouting(); 


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();