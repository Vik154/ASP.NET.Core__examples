namespace _03_BLAZOR_BASE;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Подключение razor страниц и сервера blazor
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        var app = builder.Build();

        // Добавляется middleware для обслуживания статических файлов
        // На странице _Host.cshtml подключается скрипт "_framework/blazor.server.js".
        // Поэтому для обработки запросов к этому скрипту необходимо подключить данный middleware.
        app.UseStaticFiles();

        // MapBlazorHub() связывает клиентскую часть приложения с сервером посредством соединения SignalR
        app.MapBlazorHub();

        // MapFallbackToPage("/_Host") позволяет установить страницу Razor Page по умолчанию для приложения
        // - в нашем случае страница "_Host.cshtml" из папки Pages
        app.MapFallbackToPage("/_Host");
        app.Run();
    }
}