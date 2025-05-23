using Microsoft.EntityFrameworkCore;
using NinjaManager.Data.Models;

namespace NinjaManager;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<NinjaManagerContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("NinjaManager"),
                b => b.MigrationsAssembly("NinjaManager.Data")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Ninja/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Ninja}/{action=Index}/{id?}");

        app.Run();
    }
}
