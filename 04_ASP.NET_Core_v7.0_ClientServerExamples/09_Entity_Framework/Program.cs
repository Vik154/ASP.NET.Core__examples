// ������ � ������� � Entity Framework
using _09_Entity_Framework.Models;
using Microsoft.EntityFrameworkCore;

namespace _09_Entity_Framework;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // ��������� ������ ����������� � ��
        string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
        
        // ��������� �������� ApplicationContext � �������� ������� � ����������
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

        builder.Services.AddControllersWithViews();

        var app = builder.Build();
        app.MapDefaultControllerRoute();
        app.Run();
    }
}