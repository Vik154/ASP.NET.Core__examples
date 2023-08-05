// Обработка путей конвеера Map
namespace _01_BASE_CONCEPT.Services;

// Map() применяется для создания ветки конвейера, которая будет обрабатывать запрос по определенному пути
// public static IApplicationBuilder Map (
//                  this IApplicationBuilder app,
//                  string pathMatch,
//                  Action<IApplicationBuilder> configuration);


public class _03_MiddlewareMap {

    public static void Step1(IApplicationBuilder app) {
        var time = DateTime.Now.ToShortTimeString();

        app.Use(async (context, next) => {
            Console.WriteLine($"Теущее время: {time}"); // Логги в консоли
            await next();                               // Вызов следующего middleware
            Console.WriteLine(new string('-', 30));
            Console.WriteLine($"Теущее время: {time}"); // Логги в консоли после обработки запроса следующим middleware
        });

        // Следующий middleware
        app.Run(async context => { await context.Response.WriteAsync($"Time: {time}"); });
    }
}
