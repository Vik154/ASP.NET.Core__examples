namespace BaseServer;

public class CoreSenders {
    // Minimal ASP Core API
    public static void RunApplication(string[] args) {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        WebApplication app = builder.Build();

        // Отправка форм
        app.Run(FormSending);

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
}