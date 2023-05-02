using BeneNotenrechner.Backend;

namespace BeneNotenrechner {
    public class Program {

        // TODO: Dont alow easy creation of new accounts either add question prompt or registration menu
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

            // DB
            DBManager db = new DBManager("localhost", "Hicham", "Hallosaid1", "benenotenrechner_db");
            UserManager.Start();

            app.Run();
        }

    }
}