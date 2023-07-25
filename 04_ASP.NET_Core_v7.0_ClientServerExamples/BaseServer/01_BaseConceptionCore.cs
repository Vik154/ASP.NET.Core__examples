using Microsoft.AspNetCore.Http;
using System.Reflection.PortableExecutable;

namespace BaseServer;

// Пример минимального апи на net core
public class BaseConceptionCore {

    public static void RunApplication(string[] args) {

        // Шаг 1 - Создание приложения по умолчанию начинается с класса WebApplicationBuilder.
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Шаг 2 - С помощью Build создаем объект WebApplication, который представляет приложение ASP.NET:
        // Класс WebApplication применяется для управления обработкой запроса, установки маршрутов,
        // получения сервисов и т.д.
        WebApplication app = builder.Build();

        // Шаги 3 ... N - логика (маршруты, обработчики)
        // Просто как пример -> подключение WelcomePageMiddleware
        // app.UseWelcomePage();

        // До метода Run() могут быть помещены другие методы, которые добавляют компоненты middleware.
        // В качестве параметра метод Run принимает делегат RequestDelegate.
        // Этот делегат имеет следующее определение:
        // public delegate Task RequestDelegate(HttpContext context);
        // Он принимает в качестве параметра контекст запроса HttpContext и возвращает объект Task.
        // Используем этот метод для определения простейшего компонента:
        // app.Run(async (context) => await context.Response.WriteAsync("Hello ASP CORE"));

        // Передача указателя на функцию (делегата)
        // app.Run(HandlerRequest);
        // app.Run(HtmlResponse);
        // app.Run(HeaderRequest);
        // app.Run(PathInfo);
        // app.Run(StringRequestToDict);
        // app.Run(SendMyFile);
        // app.Run(SendMyHtml);
        app.Run(MultiSendHtml);

        // Шаг N Запуск приложения
        // Run() - данный метод следует вызывать в самом конце построения конвейера обработки запроса.
        // До него же могут быть помещены другие методы, которые добавляют компоненты middleware.
        app.Run();

    } /* RunApplication(string[] args) */

    // В метод app.Run() можно передавать не только лямбды, но и полноценные функции, например чё-то вроде:
    static async Task HandlerRequest(HttpContext context) {
        await context.Response.WriteAsync("Hello ASP CORE");
    }

    // Пример отправки html-кода
    static async Task HtmlResponse(HttpContext context) {
        var response = context.Response;
        response.ContentType = "text/html; charset=utf-8";
        await response.WriteAsync("<h2>Hello World</h2><h3>Welcome to ASP CORE</h3>");
    }

    // Свойство Request объекта HttpContext представляет объект HttpRequest
    // и хранит информацию о запросе в виде свойств:
    // Получение заголовков запроса
    static async Task HeaderRequest(HttpContext context) {
        context.Response.ContentType = "text/html; charset=utf-8";
        var stringBuilder = new System.Text.StringBuilder("<table>");

        foreach (var item in context.Request.Headers)
            stringBuilder.Append($"<tr><td>{item.Key}</td><td>{item.Value}</td></tr>");

        stringBuilder.Append("</table>");
        await context.Response.WriteAsync(stringBuilder.ToString());
    }

    // Получение запрошенного пути
    static async Task PathInfo(HttpContext context) {
        var path = context.Request.Path;
        var now = DateTime.Now;
        var response = context.Response;

        if (path == "/date")
            await response.WriteAsync($"Date: {now.ToShortDateString()}");
        else if (path == "/time")
            await response.WriteAsync($"Time: {now.ToShortTimeString()}");
        else
            await response.WriteAsync("Hello world!");
    }

    // Получение всех параметров строки запроса в виде словаря: Query
    static async Task StringRequestToDict(HttpContext context) {
        context.Response.ContentType = "text/html; charset=utf-8";
        var stringBuilder = new System.Text.StringBuilder("<h3>Параметры строки запроса</h3><table>");
        stringBuilder.Append("<tr><td>Параметр</td><td>Значение</td></tr>");
        foreach (var param in context.Request.Query) {
            stringBuilder.Append($"<tr><td>{param.Key}</td><td>{param.Value}</td></tr>");
        }
        stringBuilder.Append("</table>");
        await context.Response.WriteAsync(stringBuilder.ToString());
    }

    // Отправка файлов с помощью SendFileAsync
    static async Task SendMyFile(HttpContext context) {
        await context.Response.SendFileAsync("C:\\forest.jpg");
    }

    // Отправка Html страницы
    static async Task SendMyHtml(HttpContext context) {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("html/htmlpage.html");
    }

    // Множественная отправка html страниц
    static async Task MultiSendHtml(HttpContext context) {
        var path = context.Request.Path;
        var fullPath = $"html/{path}";
        var response = context.Response;

        response.ContentType = "text/html; charset=utf-8";
        if (File.Exists(fullPath))
            await response.SendFileAsync(fullPath);
        else {
            response.StatusCode = 404;
            await response.WriteAsync("<h2>Not found</h2>");
        }
    }

    } // class BaseConceptionCore