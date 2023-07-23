// ������ �������� ������� (������ � ���������)
using System;

namespace BaseServer;


public class Program {
    public static void Main(string[] args) {
        RunApp();
    }

    // ����� �����
    static void RunApp() {
        // ������ 1
        // Example_1();
        // ������ 2
        // Example_2();
        // ������ 3 "�������� ����������"
        // Example_3();
        // ������ 4 "�������� ������"
        // Example_4();
        // ������ 5 �������� json � ������� HttpClient
        // Example_5();
        // ������ 6
        Example_6();
    }

    // ������ 1
    static void Example_1() {
        // ������� ���������� ������� ������ WebApplicationBuilder:
        var builder = WebApplication.CreateBuilder();
        // � ������� Build ������� ������ WebApplication, ������� ������������ ���������� ASP.NET:
        var app = builder.Build();
        // � ������� app.MapGet() ���������� �������� ����� - ���������� �������� �� ������������� ��������:
        app.MapGet("/", () => new Person("Tom", 38));
        // ����� ��������� ����������:
        app.Run();
    }

    // ������ 2
    static void Example_2() {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.MapGet("/{id?}", (int? id) => {
            if (id is null)
                return Results.BadRequest(new { Message = "������������ ������ � �������" });
            else if (id != 1)
                return Results.NotFound(new { Message = $"������ � id={id} �� ����������" });
            else
                return Results.Json(new Person("Bob", 42));
        });

        app.Run();
    }

    // ������ 3 "�������� ����������"
    static void Example_3() {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.MapGet("/", (HttpContext context) => {
            // �������� �������� �������� "SecreteCode"
            context.Request.Headers.TryGetValue("User-Agent", out var userAgent);
            // �������� �������� �������� "SecreteCode"
            context.Request.Headers.TryGetValue("SecreteCode", out var secreteCode);
            // �������� ������ ������� �������
            return $"User-Agent: {userAgent}  SecreteCode: {secreteCode}";
        });
        app.Run();
    }

    // ������ 4 "�������� ������"
    static void Example_4() {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.MapPost("/data", async (HttpContext httpContext) => {
            using StreamReader reader = new StreamReader(httpContext.Request.Body);
            string name = await reader.ReadToEndAsync();
            return $"�������� ������: {name}";
        });
        app.Run();
    }

    // ������ 5 �������� json � ������� HttpClient
    static void Example_5() {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.MapPost("/create", (Person2 person) => {
            // ������������� id � ������� Person
            person.Id = Guid.NewGuid().ToString();
            // ���������� ������� ������ Person
            return person;
        });
        app.Run();
    }

    // ������ 6 �������������� HttpClient � Web API
    static void Example_6() {
        // ��� ��������� id ��������
        int id = 1;

        // ��������� ������
        List<Person3> users = new List<Person3> {
            new() { Id = id++, Name = "Tom", Age = 37 },
            new() { Id = id++, Name = "Bob", Age = 41 },
            new() { Id = id++, Name = "Sam", Age = 24 }
        };

        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();

        app.MapGet("/api/users", () => users);

        app.MapGet("/api/users/{id}", (int id) => {
            // �������� ������������ �� id
            Person3? user = users.FirstOrDefault(u => u.Id == id);
            // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
            if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

            // ���� ������������ ������, ���������� ���
            return Results.Json(user);
        });

        app.MapDelete("/api/users/{id}", (int id) => {
            // �������� ������������ �� id
            Person3? user = users.FirstOrDefault(u => u.Id == id);

            // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
            if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

            // ���� ������������ ������, ������� ���
            users.Remove(user);
            return Results.Json(user);
        });

        app.MapPost("/api/users", (Person3 user) => {

            // ������������� id ��� ������ ������������
            user.Id = id++;
            // ��������� ������������ � ������
            users.Add(user);
            return user;
        });

        app.MapPut("/api/users", (Person3 userData) => {

            // �������� ������������ �� id
            var user = users.FirstOrDefault(u => u.Id == userData.Id);
            // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
            if (user == null) return Results.NotFound(new { message = "������������ �� ������" });
            // ���� ������������ ������, �������� ��� ������ � ���������� ������� �������

            user.Age = userData.Age;
            user.Name = userData.Name;
            return Results.Json(user);
        });

        app.Run();
    }
}

// ������� 1 - 4
record Person(string Name, int Age);

// ������ 5
class Person2 {
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}

// ������ 6
public class Person3 {
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}