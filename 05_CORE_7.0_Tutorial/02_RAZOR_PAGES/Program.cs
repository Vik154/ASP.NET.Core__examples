namespace _02_RAZOR_PAGES;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // ���������� razor - �������
        builder.Services.AddRazorPages();
        var app = builder.Build();

        // ��������� ������������� ��� razor - �������
        app.MapRazorPages();
        app.Run();
    }
}