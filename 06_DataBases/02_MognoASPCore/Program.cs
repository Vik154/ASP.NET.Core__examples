// Основные операции с данными в MongoDB и ASP.NET Web API
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _02_MognoASPCore;


public class Program {
    public static void Main(string[] args) {

        var client = new MongoClient("mongodb://localhost:27017");  // определяем клиент
        var db = client.GetDatabase("test");                        // определяем объект базы данных
        var collectionName = "core_users";                          // имя коллекции
        
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        app.UseDefaultFiles();
        app.UseStaticFiles();

        // Конечная точка, которая обрабатывает запрос типа GET по маршруту "api/users":
        app.MapGet("/api/users", () => {
            db.GetCollection<Person>(collectionName).Find("{}").ToListAsync();
        });

        // Конечная точка, которая обрабатывает запрос типа GET по адресу "api/users/{id}"
        // для получения одного объекта по id:
        app.MapGet("/api/users/{id}", async (string id) => {
            var user = await db.GetCollection<Person>(collectionName)
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (user == null)
                return Results.NotFound(new { message = "Пользователь не найден" });
            
            return Results.Json(user);
        });

        // При получении запроса типа DELETE по маршруту "/api/users/{id}" срабатывает эта конечная точка
        app.MapDelete("/api/users/{id}", async (string id) => {
            var user = await db.GetCollection<Person>(collectionName)
                .FindOneAndDeleteAsync(p => p.Id == id);

            if (user is null) 
                return Results.NotFound(new { message = "Пользователь не найден" });

            return Results.Json(user);
        });

        // При получении запроса с методом POST по адресу "/api/users" срабатывает следующая конечная точка:
        app.MapPost("/api/users", async (Person user) => {
            await db.GetCollection<Person>(collectionName).InsertOneAsync(user);
            return user;
        });

        app.MapPut("/api/users", async (Person userData) => {
            var user = await db.GetCollection<Person>(collectionName)
                .FindOneAndReplaceAsync(p =>
                    p.Id == userData.Id,
                    userData,
                    new() { ReturnDocument = ReturnDocument.After }
                );

            if (user == null)
                return Results.NotFound(new { message = "Пользователь не найден" });

            return Results.Json(user);
        });

        app.Run();
    }
}

public class Person {
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id   { get; set; } = "";
    public string Name { get; set; } = "";
    public int    Age  { get; set; }
}