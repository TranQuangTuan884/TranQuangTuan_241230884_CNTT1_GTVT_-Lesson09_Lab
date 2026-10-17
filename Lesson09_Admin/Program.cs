using Microsoft.EntityFrameworkCore;
using Lesson09_Lab.Areas.Admin.Data;
namespace Lesson09_Lab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AdminDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("AdminDb") ?? "Data Source=admin.db"));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope()) { var db = scope.ServiceProvider.GetRequiredService<AdminDbContext>(); db.Database.EnsureCreated(); }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            // 1. THÊM ROUTE CHO AREA ADMIN VÀO ĐÂY (Phải đặt trên route default)
            app.MapControllerRoute(
                name: "MyArea",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}")
                .WithStaticAssets();

            // 2. Route default mặc định của hệ thống
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}