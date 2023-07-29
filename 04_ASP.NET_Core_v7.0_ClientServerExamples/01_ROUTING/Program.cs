namespace Routing;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        // builder.Services.AddControllers();        // ��������� ��������� ������������
        builder.Services.AddControllersWithViews();  // ��������� ��������� ������������ � ���������������
        var app = builder.Build();

        // ����������� �������� ����� ����������� � ������
        app.MapControllerRoute(
            name: "TwoParametrs",
            pattern: "{controller}/{action}/{x}/{y}");
        // ������������� ������������� ��������� � �������������
        // ����� ����� ������� ����������� � ����� ������
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}