namespace _02_RAZOR_PAGES;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Добавление razor - страниц
        builder.Services.AddRazorPages();
        var app = builder.Build();

        // Поддержка маршрутизации для razor - страниц
        app.MapRazorPages();
        app.Run();
    }
}