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
        // Example_5();
        // Пример 6
        // Example_6();
        // Пример 7
        // Example_7();
        // Пример 8 Отправка потоков и массива байтов
        Example_8();
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

    // Пример 6 Взаимодействие HttpClient с Web API
    static void Example_6() {
        // для генерации id объектов
        int id = 1;

        // начальные данные
        List<Person3> users = new List<Person3> {
            new() { Id = id++, Name = "Tom", Age = 37 },
            new() { Id = id++, Name = "Bob", Age = 41 },
            new() { Id = id++, Name = "Sam", Age = 24 }
        };

        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.MapGet("/api/users", () => users);

        app.MapGet("/api/users/{id}", (int id) => {
            // получаем пользователя по id
            Person3? user = users.FirstOrDefault(u => u.Id == id);
            // если не найден, отправляем статусный код и сообщение об ошибке
            if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

            // если пользователь найден, отправляем его
            return Results.Json(user);
        });

        app.MapDelete("/api/users/{id}", (int id) => {
            // получаем пользователя по id
            Person3? user = users.FirstOrDefault(u => u.Id == id);

            // если не найден, отправляем статусный код и сообщение об ошибке
            if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

            // если пользователь найден, удаляем его
            users.Remove(user);
            return Results.Json(user);
        });

        app.MapPost("/api/users", (Person3 user) => {

            // устанавливаем id для нового пользователя
            user.Id = id++;
            // добавляем пользователя в список
            users.Add(user);
            return user;
        });

        app.MapPut("/api/users", (Person3 userData) => {

            // получаем пользователя по id
            var user = users.FirstOrDefault(u => u.Id == userData.Id);
            // если не найден, отправляем статусный код и сообщение об ошибке
            if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
            // если пользователь найден, изменяем его данные и отправляем обратно клиенту

            user.Age = userData.Age;
            user.Name = userData.Name;
            return Results.Json(user);
        });

        app.Run();
    }

    // Пример 7 Отправка форм
    static void Example_7() {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.MapPost("/data", async (HttpContext httpContext) => {
            // получаем данные формы
            var form = httpContext.Request.Form;
            string? name = form["name"];
            string? email = form["email"];
            string? age = form["age"];
            await httpContext.Response.WriteAsync($"Name: {name}   Email:{email}    Age: {age}");
        });
        app.Run();
    }

    // Пример 8 Отправка потоков и массива байтов
    static void Example_8() {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.MapPost("/data", async (HttpContext httpContext) => {
            // путь к папке, где будут храниться файлы
            var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
            // создаем папку для хранения файлов
            Directory.CreateDirectory(uploadPath);
            // генерируем произвольное название файла с помощью guid
            string fileName = Guid.NewGuid().ToString();
            // получаем поток
            using (var fileStream = new FileStream($"{uploadPath}/{fileName}.jpg", FileMode.Create)) {
                await httpContext.Request.Body.CopyToAsync(fileStream);
            }

            await httpContext.Response.WriteAsync("Данные сохранены");
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

// Пример 6
public class Person3 {
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}