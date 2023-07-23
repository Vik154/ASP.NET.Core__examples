// Пример простого сервера (работа с запросами)
using System;

namespace BaseServer;


public class Program {
    public static void Main(string[] args) {
        RunApp();
    }

    // Точка входа
    static void RunApp() {
        // Пример 1
        // Example_1();
        // Пример 2
        // Example_2();
        // Пример 3 "Отправка заголовков"
        // Example_3();
        // Пример 4 "Отправка текста"
        // Example_4();
        // Пример 5 Отправка json с помощью HttpClient
        Example_5();
    }

    // Пример 1
    static void Example_1() {
        // сначала необходимо создать объект WebApplicationBuilder:
        var builder = WebApplication.CreateBuilder();
        // с помощью Build создаем объект WebApplication, который представляет приложение ASP.NET:
        var app = builder.Build();
        // С помощью app.MapGet() определяем конечную точку - обработчик запросов по определенному маршруту:
        app.MapGet("/", () => new Person("Tom", 38));
        // Затем запускаем приложение:
        app.Run();
    }

    // Пример 2
    static void Example_2() {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.MapGet("/{id?}", (int? id) => {
            if (id is null)
                return Results.BadRequest(new { Message = "Некорректные данные в запросе" });
            else if (id != 1)
                return Results.NotFound(new { Message = $"Объект с id={id} не существует" });
            else
                return Results.Json(new Person("Bob", 42));
        });

        app.Run();
    }

    // Пример 3 "Отправка заголовков"
    static void Example_3() {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.MapGet("/", (HttpContext context) => {
            // Пытаемся получить заголвок "SecreteCode"
            context.Request.Headers.TryGetValue("User-Agent", out var userAgent);
            // Пытаемся получить заголвок "SecreteCode"
            context.Request.Headers.TryGetValue("SecreteCode", out var secreteCode);
            // Отправка данных обратно клиенту
            return $"User-Agent: {userAgent}  SecreteCode: {secreteCode}";
        });
        app.Run();
    }

    // Пример 4 "Отправка текста"
    static void Example_4() {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.MapPost("/data", async (HttpContext httpContext) => {
            using StreamReader reader = new StreamReader(httpContext.Request.Body);
            string name = await reader.ReadToEndAsync();
            return $"Получены данные: {name}";
        });
        app.Run();
    }

    // Пример 5 Отправка json с помощью HttpClient
    static void Example_5() {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.MapPost("/create", (Person2 person) => {
            // устанавливает id у объекта Person
            person.Id = Guid.NewGuid().ToString();
            // отправляем обратно объект Person
            return person;
        });
        app.Run();
    }
}

// Примеры 1 - 4
record Person(string Name, int Age);

// Пример 5
class Person2 {
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}