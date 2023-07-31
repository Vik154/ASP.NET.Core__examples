using _05_MODELS.Infrastructure;

namespace _05_MODELS;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // добавление провайдера CustomDateTime в коллекцию провайдеров привязчиков модели.
        builder.Services.AddControllersWithViews(opts => {
            opts.ModelBinderProviders.Insert(0, new CustomDateTimeModelBinderProvider());
        });

        var app = builder.Build();

        app.MapControllerRoute(name: "default", pattern: "{controller=Event}/{action=Create}/{id?}");

        app.Run();
    }
}