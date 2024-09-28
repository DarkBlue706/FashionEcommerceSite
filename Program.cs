using Microsoft.AspNetCore.HttpsPolicy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//add services to con


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


// these are the middleware components
// app.MapGet("/products", () => 
// {
//     return "Reading products";
// });

// app.MapGet("/products/{id}", (int id) =>
// {

//     return "Reading product with ID: {id}";
// });

// app.MapPost("/products", () => 
// {
//     return $"Creating your products";
// });

// app.MapPut("/products/{id}", (int id) =>
// {
//     return $"Updating product with ID: {id}";
// });

// app.MapDelete("/products/{id}", (int id) =>
// {
//     return $"Deleting product with ID: {id}";
// });