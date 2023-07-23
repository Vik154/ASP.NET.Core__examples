// ������ �������� ������� (������ � ���������)
using System;

namespace BaseServer;


public class Program {
    public static void Main(string[] args) {
        // ������ 1        Example_1();
        // ������ 2
        Example_2();
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

        app.MapGet("/{id?}", (int? id) =>
        {
            if (id is null)
                return Results.BadRequest(new { Message = "������������ ������ � �������" });
            else if (id != 1)
                return Results.NotFound(new { Message = $"������ � id={id} �� ����������" });
            else
                return Results.Json(new Person("Bob", 42));
        });

        app.Run();
    }
}


record Person(string Name, int Age);