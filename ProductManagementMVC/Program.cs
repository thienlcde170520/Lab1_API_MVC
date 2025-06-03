using Repositories.Interfaces;
using Repositories.Repository;
using Services.Interfaces;
using Services.Service;

namespace ProductManagementMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<ICatergoryRepository, CatergoryRepository>();
            builder.Services.AddSingleton<ICatergoryService, CategoryService>();

            builder.Services.AddHttpClient("ProductApi", client =>
            {
                // ĐÂY là URL project ProductWebAPI của bạn – chỉnh lại nếu port khác
                client.BaseAddress = new Uri("https://localhost:5054/api/");
            });

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
