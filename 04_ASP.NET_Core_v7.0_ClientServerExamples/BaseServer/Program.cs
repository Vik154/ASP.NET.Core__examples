// ������ �������� ������� (������ � ���������)
using System;

namespace BaseServer;


public class Program {
    public static void Main(string[] args) {
        // ������ 1
        // Example_1();
        // ������ 2
        // Example_2();
        // ������ 3 "�������� ����������"
        Example_3();
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
}


record Person(string Name, int Age);