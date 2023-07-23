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
        Example_5();
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
}

// ������� 1 - 4
record Person(string Name, int Age);

// ������ 5
class Person2 {
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}