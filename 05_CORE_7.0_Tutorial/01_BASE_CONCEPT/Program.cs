using _01_BASE_CONCEPT.Services;
namespace _01_BASE_CONCEPT;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        // var app = builder.Build();

        // 05 - Уравление жизненным циклом сервисов.
        // AddSingleton создает один объект для всех последующих запросов,
        // при этом объект создается только тогда, когда он непосредственно необходим
        builder.Services.AddSingleton<ICounter, RandomCounter>();
        builder.Services.AddSingleton<CounterService>();

        // AddScoped создает один экземпляр объекта для всего запроса
        // builder.Services.AddScoped<ICounter, RandomCounter>();
        // builder.Services.AddScoped<CounterService>();

        // AddTransient() создает transient-объекты. Такие объекты создаются при каждом обращении к ним
        // builder.Services.AddTransient<ICounter, RandomCounter>();
        // builder.Services.AddTransient<CounterService>();
        var app = builder.Build();
        app.UseMiddleware<CounterMiddleware>();

        // 04 - Middleware в классах
        // Для добавления компонента middleware, который представляет класс,
        // в конвейер обработки запроса применяется метод UseMiddleware().
        // app.UseMiddleware<_04_MiddlewareClass>();
        // app.Run(async context => await context.Response.WriteAsync("Hello"));

        // 03 - Map создание ветки конвейра которая будет обрабатывать запросы по указанному пути
        // app.Map("/time", _03_MiddlewareMap.Step1);
        // app.Map("/About", ap => ap.Run(async ct => await ct.Response.WriteAsync("About")));
        // app.Map("/Index", ap => ap.Run(async ct => await ct.Response.WriteAsync("Index")));
        // app.Run(async ct => await ct.Response.WriteAsync("Hello Timer"));

        // 02 - UseWhen / MapWhen создание ветки конвеера middleware
        // app.MapWhen(_02_MiddlewareUseWhen.Step1, _02_MiddlewareUseWhen.Step2);
        // app.UseWhen(_02_MiddlewareUseWhen.Step1, _02_MiddlewareUseWhen.Step2);
        // app.Run(_02_MiddlewareUseWhen.Step3);

        // 01 - Use - Передача обработки запроса следующим компонетам конвеера middleware
        // app.Use(_01_MiddlewareUse.Step3);
        // app.Run(_01_MiddlewareUse.Step4);
        // app.Use(_01_MiddlewareUse.Step1);
        // app.Run(_01_MiddlewareUse.Step2);

        // 01 - Отправка форм
        // app.Run(SenderForm.SendHtmlForm);
        // <----> Одно и тоже
        /*app.Run(async context => {
            context.Response.ContentType = "text/html";
            await context.Response.SendFileAsync("html/htmlpage.html");
        });
        */
        app.Run();
    }
}

/*
 * 1 - USE:
 
var builder = WebApplication.CreateBuilder();
var app = builder.Build();
 
string date = "";
 
app.Use(async(context, next) => {
    date = DateTime.Now.ToShortDateString();     // Действия до передачи запроса дальше по конвееру 
    await next.Invoke();                         // вызываем middleware из app.Run
    Console.WriteLine($"Current date: {date}");  // Действия после обработки запроса следующим middleware
});
 
// Следующий middleware
app.Run(async(context) => await context.Response.WriteAsync($"Date: {date}"));
 
app.Run();
 
 */