namespace _08_VIEW_COMPONENTS;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        var app = builder.Build();

        app.MapControllerRoute(
            name: "PersonSample", 
            pattern: "{controller=Home}/{action=TestPersonComponent}");
          
        /*
        app.MapControllerRoute(
            name: "TimerSample", 
            pattern: "{controller=Home}/{action=TestTimerComponent}");
        */

        app.Run();
    }
}