using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;

namespace _06_HTMLHellpers;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        var app = builder.Build();

        // app.MapControllerRoute(name: "ListHelper", pattern: "{controller=ListHelper}/{action=Index}");
        app.MapControllerRoute(name: "HtmlHelper", pattern: "{controller=Base}/{action=Create}");

        app.Run();
    }
}