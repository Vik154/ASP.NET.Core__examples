using System.ComponentModel;
using System.Text.Json;

namespace BaseServer;

public class CoreSenders {
    // Minimal ASP Core API
    public static void RunApplication(string[] args) {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        WebApplication app = builder.Build();

        // Отправка форм
        // app.Run(FormSending);

        // Отправка Json
        // app.Run(JsonSender);

        // Json конвертер
        app.Run(JsonExceptionHandler);

        app.Run();
    }

    // Отправка форм
    static async Task FormSending(HttpContext context) {
        context.Response.ContentType = "text/html; charset=utf-8";
        
        // если обращение идет по адресу "/postuser", получаем данные формы
        if (context.Request.Path == "/postuser") {
            var form = context.Request.Form;
            string? name = form["name"];
            string? age = form["age"];
            string[]? languages = form["languages"];
            
            // создаем из массива languages одну строку
            string langList = "";
            foreach (var lang in languages) {
                langList += $" {lang}";
            }
            await context.Response.WriteAsync($"<div><p>Name: {name}</p>" +
                $"<p>Age: {age}</p>" +
                $"<div>Languages:{langList}</div></div>");
        }
        else {
            await context.Response.SendFileAsync("html/index.html");
        }
    }

    // Отправка JSON. Методы WriteAsJsonAsync ReadFromJsonAsync
    static async Task JsonSender(HttpContext context) {
        var response = context.Response;
        var request = context.Request;
        
        if (request.Path == "/api/user") {
            var message = "Некорректные данные";   // содержание сообщения по умолчанию
            try {
                // пытаемся получить данные json
                var person = await request.ReadFromJsonAsync<Person>();
                if (person != null) // если данные сконвертированы в Person
                    message = $"Name: {person.Name}  Age: {person.Age}";
            }
            catch { }
            // отправляем пользователю данные
            await response.WriteAsJsonAsync(new { text = message });
        }
        else {
            response.ContentType = "text/html; charset=utf-8";
            await response.SendFileAsync("html/IndexJson.html");
        }
    }

    // Решение проблемы с исключением типа System.Text.Json.JsonException
    static async Task JsonExceptionHandler(HttpContext context) {
        var response = context.Response;
        var request = context.Request;
        
        if (request.Path == "/api/user") {
            var responseText = "Некорректные данные";   // содержание сообщения по умолчанию

            if (request.HasJsonContentType()) {
                // определяем параметры сериализации/десериализации
                var jsonoptions = new JsonSerializerOptions();
                // добавляем конвертер кода json в объект типа Person
                jsonoptions.Converters.Add(new PersonConverter());
                // десериализуем данные с помощью конвертера PersonConverter
                var person = await request.ReadFromJsonAsync<Person>(jsonoptions);
                if (person != null)
                    responseText = $"Name: {person.Name}  Age: {person.Age}";
            }
            await response.WriteAsJsonAsync(new { text = responseText });
        }
        else {
            response.ContentType = "text/html; charset=utf-8";
            await response.SendFileAsync("html/IndexJson.html");
        }
    }
}