using _04_VIEW_COMPONENTS.Util;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace _04_VIEW_COMPONENTS;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        
        // ������������� ������ �������������
        builder.Services.Configure<MvcViewOptions>(options => {
            options.ViewEngines.Clear();                            // ������� ������� ��� ��������� �������
            options.ViewEngines.Insert(0, new CustomViewEngine());  // ��������� � ������ ��������� ��� ������
        });

        var app = builder.Build();

        // app.MapGet("/", () => "Hello World!");
        app.MapControllerRoute(
            name: "default", 
            pattern: "{controller=Engine}/{action=Index}");

        app.Run();
    }
}