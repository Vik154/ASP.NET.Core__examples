namespace Routing;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        // builder.Services.AddControllers();        // добавляем поддержку контроллеров
        builder.Services.AddControllersWithViews();  // добавляем поддержку контроллеров с представлениями
        var app = builder.Build();

        // Специфичные маршруты лучше расположить в начале
        app.MapControllerRoute(
            name: "TwoParametrs",
            pattern: "{controller}/{action}/{x}/{y}");
        // устанавливаем сопоставление маршрутов с контроллерами
        // Более общий маршрут распологают в конце списка
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}