// Создание ветки конвейера. UseWhen и MapWhen
namespace _01_BASE_CONCEPT.Services;

// UseWhen() на основании некоторого условия позволяет создать ответвление конвейера при обработке запроса
// public static IApplicationBuilder UseWhen (
//               this IApplicationBuilder app,
//               Func<HttpContext,bool> predicate,
//               Action<IApplicationBuilder> configuration);

// MapWhen(), как и метод UseWhen(), на основании некоторого условия позволяет создать ответвление конвейера:
// public static IApplicationBuilder MapWhen (
//               this IApplicationBuilder app,
//               Func<HttpContext,bool> predicate,
//               Action<IApplicationBuilder> configuration);

public class _02_MiddlewareUseWhen {
    
    // ШАГ 1
    public static bool Step1(HttpContext context) {
        string? result = context.Request.Path;
        if (result ==  "/time") {
            Console.WriteLine("Request Returned TRUE");
            return true;
        }
        Console.WriteLine("Request Returned TRUE");
        return false;
    }

    // ШАГ 2
    public static void Step2(IApplicationBuilder appBuilder) {
        
        // 1 - логгируем данные - выводим на консоль приложения
        appBuilder.Use(async (context, next) => {
            var time = DateTime.Now.ToShortTimeString();
            Console.WriteLine($"Время: {time}");
            await next();
        });

        // 2 - отправляем ответ
        appBuilder.Run(async (context) => {
            var time = DateTime.Now.ToShortTimeString();
            await context.Response.WriteAsync($"Time: {time}");
        });
    }

    // ШАГ 3
    public static async Task Step3(HttpContext context) {
        await context.Response.WriteAsync("Hello World!");
    }
}
