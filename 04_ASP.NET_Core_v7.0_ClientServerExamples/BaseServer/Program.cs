// ������ �������� ������� (������ � ���������)
using System;

namespace BaseServer;


public class Program {
    public static void Main(string[] args) {
        // ������� ���������� ������� ������ WebApplicationBuilder:
        var builder = WebApplication.CreateBuilder();
        // � ������� Build ������� ������ WebApplication, ������� ������������ ���������� ASP.NET:
        var app = builder.Build();
        // � ������� app.MapGet() ���������� �������� ����� - ���������� �������� �� ������������� ��������:
        app.MapGet("/", () => new Person("Tom", 38));
        // ����� ��������� ����������:
        app.Run();
    }
}


record Person(string Name, int Age);