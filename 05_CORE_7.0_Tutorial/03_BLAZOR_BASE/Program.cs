namespace _03_BLAZOR_BASE;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // ����������� razor ������� � ������� blazor
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        var app = builder.Build();

        // ����������� middleware ��� ������������ ����������� ������
        // �� �������� _Host.cshtml ������������ ������ "_framework/blazor.server.js".
        // ������� ��� ��������� �������� � ����� ������� ���������� ���������� ������ middleware.
        app.UseStaticFiles();

        // MapBlazorHub() ��������� ���������� ����� ���������� � �������� ����������� ���������� SignalR
        app.MapBlazorHub();

        // MapFallbackToPage("/_Host") ��������� ���������� �������� Razor Page �� ��������� ��� ����������
        // - � ����� ������ �������� "_Host.cshtml" �� ����� Pages
        app.MapFallbackToPage("/_Host");
        app.Run();
    }
}