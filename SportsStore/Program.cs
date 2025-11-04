using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();          //Repository Dependency Injection
builder.Services.AddDbContext<StoreDbContext>((options => options.UseSqlServer(builder.Configuration.GetConnectionString("SportsStoreConnection"))));

var app = builder.Build();

app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
