using _01_BASE_CONCEPT.Services;
namespace _01_BASE_CONCEPT;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        // 04 - Middleware � �������
        // ��� ���������� ���������� middleware, ������� ������������ �����,
        // � �������� ��������� ������� ����������� ����� UseMiddleware().
        app.UseMiddleware<_04_MiddlewareClass>();
        app.Run(async context => await context.Response.WriteAsync("Hello"));

        // 03 - Map �������� ����� �������� ������� ����� ������������ ������� �� ���������� ����
        // app.Map("/time", _03_MiddlewareMap.Step1);
        // app.Map("/About", ap => ap.Run(async ct => await ct.Response.WriteAsync("About")));
        // app.Map("/Index", ap => ap.Run(async ct => await ct.Response.WriteAsync("Index")));
        // app.Run(async ct => await ct.Response.WriteAsync("Hello Timer"));

        // 02 - UseWhen / MapWhen �������� ����� �������� middleware
        // app.MapWhen(_02_MiddlewareUseWhen.Step1, _02_MiddlewareUseWhen.Step2);
        // app.UseWhen(_02_MiddlewareUseWhen.Step1, _02_MiddlewareUseWhen.Step2);
        // app.Run(_02_MiddlewareUseWhen.Step3);

        // 01 - Use - �������� ��������� ������� ��������� ���������� �������� middleware
        // app.Use(_01_MiddlewareUse.Step3);
        // app.Run(_01_MiddlewareUse.Step4);
        // app.Use(_01_MiddlewareUse.Step1);
        // app.Run(_01_MiddlewareUse.Step2);

        // 01 - �������� ����
        // app.Run(SenderForm.SendHtmlForm);

        app.Run();
    }
}

/*
 * 1 - USE:
 
var builder = WebApplication.CreateBuilder();
var app = builder.Build();
 
string date = "";
 
app.Use(async(context, next) => {
    date = DateTime.Now.ToShortDateString();     // �������� �� �������� ������� ������ �� �������� 
    await next.Invoke();                         // �������� middleware �� app.Run
    Console.WriteLine($"Current date: {date}");  // �������� ����� ��������� ������� ��������� middleware
});
 
// ��������� middleware
app.Run(async(context) => await context.Response.WriteAsync($"Date: {date}"));
 
app.Run();
 
 */