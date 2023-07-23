// Пример простого сервера (работа с запросами)
using System;

namespace BaseServer;


public class Program {
    public static void Main(string[] args) {
        // Пример 1        Example_1();
        // Пример 2
        Example_2();
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

        app.MapGet("/{id?}", (int? id) =>
        {
            if (id is null)
                return Results.BadRequest(new { Message = "Некорректные данные в запросе" });
            else if (id != 1)
                return Results.NotFound(new { Message = $"Объект с id={id} не существует" });
            else
                return Results.Json(new Person("Bob", 42));
        });

        app.Run();
    }
}


record Person(string Name, int Age);