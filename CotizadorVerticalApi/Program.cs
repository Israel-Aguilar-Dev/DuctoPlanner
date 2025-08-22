using CotizadorVerticalApi.Data;
using CotizadorVerticalApi.Facades;
using CotizadorVerticalApi.Interfaces;
using CotizadorVerticalApi.Services;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

var services = builder.Services;
var configuration = builder.Configuration;

services.AddTransient<string>(sp => configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddTransient<IQuoteRepository, QuoteRepository>();
builder.Services.AddTransient<ICatalogRepository, CatalogRepository>();
builder.Services.AddTransient<IQuoterFacade, QuoterService>();
builder.Services.AddTransient<ICatalogFacade, CatalogService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
