namespace Routing;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        // builder.Services.AddControllers();        // ��������� ��������� ������������
        builder.Services.AddControllersWithViews();  // ��������� ��������� ������������ � ���������������
        var app = builder.Build();

        // app.UseDeveloperExceptionPage();

        // ����������� �������� ����� ����������� � ������
        app.MapControllerRoute(
            name: "TwoParametrs",
            pattern: "{controller}/{action}/{x}/{y}");

        // {*data} - catch all ��������, ������� �������� � ��� �������, ����� ���������� �� ��������
        // ���������� ���������. �������� catch all ���������, ����� ��������������� ���������� ������
        // URL ������, ������� �� ������� ��� ������ ���������
        // /home/values/10/20 � ��������� {*data} ����� ���������� �������� "10/20"
        app.MapControllerRoute(
            name: "CatchAll",
            pattern: "{controller=Home}/{action}/{*data}"
            );

        // ������������� ������������� ��������� � �������������
        // ����� ����� ������� ����������� � ����� ������
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}