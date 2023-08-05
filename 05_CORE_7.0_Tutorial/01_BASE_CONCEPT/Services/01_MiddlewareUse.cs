// Пример работы метода IApplicationBuilder Use
// Передача обработки запроса следующим компонетам конвеера middleware
namespace _01_BASE_CONCEPT.Services;

// Метод расширения Use() имеет следующие версии:
// public static IApplicationBuilder Use(this IApplicationBuilder app, Func<HttpContext, Func<Task>, Task> middleware);
// public static IApplicationBuilder Use(this IApplicationBuilder app, Func<HttpContext, RequestDelegate, Task> middleware)
public class _01_MiddlewareUse {

    private static string Date { get; set; } = "";

    // Передается в метод Use
    public static async Task Step1(HttpContext context, Func<Task> next) {
        Date = DateTime.Now.ToShortDateString();    // действия перед передачей запроса в следующий middleware
        await next.Invoke();                        // Передача запроса дальше по конвееру в app.Run
        Console.WriteLine($"Текущая дата: {Date}"); // действия после обработки запроса следующим middleware
        Console.WriteLine($"Context: {context.Request.Path.Value}");
    }

    // Передатся в метод Run
    public static async Task Step2(HttpContext context) {
        await context.Response.WriteAsync($"Date: {Date}");
    }

    // Использование метода Use как терминального компонента middleware
    public static async Task Step3(HttpContext context, Func<Task> next) {

        string? path = context.Request.Path.Value?.ToLower();

        if (path == "/date")
            await context.Response.WriteAsync($"Date: {DateTime.Now.ToShortDateString()}");
        else
            await next.Invoke();
    }

    public static async Task Step4(HttpContext context) {
        await context.Response.WriteAsync("Hello World");
    }
}
