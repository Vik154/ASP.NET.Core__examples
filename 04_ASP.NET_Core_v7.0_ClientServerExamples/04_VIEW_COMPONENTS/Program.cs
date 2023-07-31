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
        app.MapControllerRoute(
            name: "default", 
            pattern: "{controller=Engine}/{action=Index}");

        app.Run();
    }
}