namespace _08_VIEW_COMPONENTS;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddTransient<ITimeService, SimpleTimeService>();
        builder.Services.AddControllersWithViews();
        var app = builder.Build();

        // Внедрение зависимостей
        app.MapControllerRoute(
            name: "DependencyTimer",
            pattern: "{controller=Home}/{action=DependencyTimer}/{id?}");

        /*
        app.MapControllerRoute(
            name: "PersonSample", 
            pattern: "{controller=Home}/{action=TestPersonComponent}");
        */
        /*
        app.MapControllerRoute(
            name: "TimerSample", 
            pattern: "{controller=Home}/{action=TestTimerComponent}");
        */

        app.Run();
    }
}

// Пример внедрения зависимостей
public interface ITimeService {
    string GetTime();
}

public class SimpleTimeService : ITimeService {
    public string GetTime() => DateTime.Now.ToString("HH:mm:ss");
}