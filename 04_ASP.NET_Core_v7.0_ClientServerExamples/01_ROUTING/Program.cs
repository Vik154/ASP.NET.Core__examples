namespace Routing;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        // builder.Services.AddControllers();        // добавляем поддержку контроллеров
        builder.Services.AddControllersWithViews();  // добавляем поддержку контроллеров с представлениями
        var app = builder.Build();

        // app.UseDeveloperExceptionPage();

        // Специфичные маршруты лучше расположить в начале
        app.MapControllerRoute(
            name: "TwoParametrs",
            pattern: "{controller}/{action}/{x}/{y}");

        // {*data} - catch all параметр, которых подходит в тех случаях, когда изначально не известно
        // количество сегментов. значение catch all параметра, будет соответствовать оставшейся строке
        // URL адреса, которая не подошла под другие параметры
        // /home/values/10/20 в параметре {*data} будет находиться значение "10/20"
        app.MapControllerRoute(
            name: "CatchAll",
            pattern: "{controller=Home}/{action}/{*data}"
            );

        // устанавливаем сопоставление маршрутов с контроллерами
        // Более общий маршрут распологают в конце списка
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}