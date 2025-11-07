using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();          //Repository Dependency Injection
builder.Services.AddDbContext<StoreDbContext>((options => options.UseSqlServer(builder.Configuration.GetConnectionString("SportsStoreConnection"))));

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute("catpage", "{category}/Page{productPage:int}", new { Controller = "Home", action = "Index" });
app.MapControllerRoute("page", "Page{productPage:int}", new { Controller = "Home", action = "Index", productPage = 1 });
app.MapControllerRoute("category", "{category}", new { Controller = "Home", action = "Index", productPage = 1 });
app.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "Index", productPage = 1 });

app.MapDefaultControllerRoute();

//SeedData.EnsurePopulated(app);
//If you need to reset the DB, run the following in the SportsStore folder:
//dotnet ef database drop --force --context StoreDbContext

app.Run();
