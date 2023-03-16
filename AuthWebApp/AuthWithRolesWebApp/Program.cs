using AuthWithRolesWebApp.Context;
using AuthWithRolesWebApp.Models;
using AuthWithRolesWebApp.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//ahmad081177- start
string path = Directory.GetCurrentDirectory();
builder.Services.AddDbContext<AuthDbContext>(options =>
        options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    .Replace("[DataDirectory]", path)));

builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<AppState>();
//ahmad081177 - end

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//ahmad081177 - start
app.UseSession();
//ahmad081177 - end

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
