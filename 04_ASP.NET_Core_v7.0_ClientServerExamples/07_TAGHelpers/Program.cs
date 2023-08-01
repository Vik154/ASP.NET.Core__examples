namespace _07_TAGHelpers;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        var app = builder.Build();

        // app.MapControllerRoute(name: "Default", pattern: "{controller=Home}/{action=Index}");
        // app.MapControllerRoute(name: "Default", pattern: "{controller=Home}/{action=Create}");
        // app.MapControllerRoute(name: "Default", pattern: "{controller=Home}/{action=Days}");
        // app.MapControllerRoute(name: "Default", pattern: "{controller=Home}/{action=THelp}");
        // app.MapControllerRoute(name: "Default", pattern: "{controller=Home}/{action=AsyncTHelp}");
        // app.MapControllerRoute(name: "Default", pattern: "{controller=Home}/{action=Advanced}");
        app.MapControllerRoute(name: "Default", pattern: "{controller=Home}/{action=MyStyle}");

        app.Run();
    }
}