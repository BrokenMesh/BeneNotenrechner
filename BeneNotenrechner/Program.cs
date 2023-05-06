using BeneNotenrechner.Backend;
using System.Text.Json;

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

            /*
            // Testing - SampleData
            //Profile _p = db.CreateProfile(3);
            Profile _p = db.GetProfile(3, false);

            //db.CreateSuperSubject(_p, "Demo1", 1);
            //db.CreateSuperSubject(_p, "Demo2", 1);
            //db.CreateSuperSubject(_p, "Demo3", 1);

            List<SuperSubject> _superSubjects = db.GetSuperSubjectAll(_p, false);

            foreach (SuperSubject superSubject in _superSubjects) {
                //db.CreateSubject(superSubject, "ABU");
                //db.CreateSubject(superSubject, "M122");
                //db.CreateSubject(superSubject, "M320");

                List<Subject> _subjects = db.GetSubjectAll(superSubject, false);

                foreach (Subject _subject in _subjects) {
                    db.CreateGrade(_subject, 5.5f, DateTime.Now, "ZP1");
                    db.CreateGrade(_subject, 5.0f, DateTime.Now, "ZP2");
                    db.CreateGrade(_subject, 4.5f, DateTime.Now, "ZP3");
                    db.CreateGrade(_subject, 3.5f, DateTime.Now, "ZP4");
                    db.CreateGrade(_subject, 1.0f, DateTime.Now, "ZP5");
                }

            }*/

            app.Run();
        }

    }
}