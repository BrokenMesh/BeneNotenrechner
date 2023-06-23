using BeneNotenrechner.Backend;
using System.Text.Json;

namespace BeneNotenrechner {
    public class Program {

        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");
            ;

            // Load Config
            Config _config = Config.LoadConfig("./config.txt");

            // Backend
            DBManager db = new DBManager(_config);
            EMailManager em = new EMailManager(_config);
            UserManager.Start();

            app.Run();
        }

    }
}