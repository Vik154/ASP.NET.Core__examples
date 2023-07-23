// Пример простого сервера (работа с запросами)
using System;

namespace BaseServer;


public class Program {
    public static void Main(string[] args) {
        // сначала необходимо создать объект WebApplicationBuilder:
        var builder = WebApplication.CreateBuilder();
        // с помощью Build создаем объект WebApplication, который представляет приложение ASP.NET:
        var app = builder.Build();
        // С помощью app.MapGet() определяем конечную точку - обработчик запросов по определенному маршруту:
        app.MapGet("/", () => new Person("Tom", 38));
        // Затем запускаем приложение:
        app.Run();
    }
}


record Person(string Name, int Age);