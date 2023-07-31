using _04_VIEW_COMPONENTS.Util;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace _04_VIEW_COMPONENTS;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        
        // устанавливаем движок представлений
        builder.Services.Configure<MvcViewOptions>(options => {
            options.ViewEngines.Clear();                            // сначала очищаем всю коллекцию движков
            options.ViewEngines.Insert(0, new CustomViewEngine());  // добавляем в начало коллекции наш движок
        });

        var app = builder.Build();

        // app.MapGet("/", () => "Hello World!");
        /* app.MapControllerRoute(
            name: "default", 
            pattern: "{controller=Engine}/{action=Index}");
        */

        // Маршрутизация на основе атрибутов
        // app.MapControllers();

        /* ОБЛАСТИ - AREAS */
        // добавляем поддержку контроллеров, которые располагаются в области
        app.MapControllerRoute(
            name: "Acc",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        // добавляем поддержку для контроллеров, которые располагаются вне области
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}