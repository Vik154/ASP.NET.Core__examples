// Классы middleware
namespace _01_BASE_CONCEPT.Services;

// Класс middleware должен иметь конструктор, который принимает параметр типа RequestDelegate.
// Через этот параметр можно получить ссылку на тот делегат запроса, который стоит следующим в конвейере обработки запроса.
public class _04_MiddlewareClass {

    private readonly RequestDelegate _next;
    public _04_MiddlewareClass(RequestDelegate next) => _next = next;

    // в классе должен быть определен метод, который должен называться либо Invoke, либо InvokeAsync.
    // Причем этот метод должен возвращать объект Task и принимать в качестве параметра
    // контекст запроса - объект HttpContext. Данный метод собственно и будет обрабатывать запрос.
    public async Task InvokeAsync(HttpContext context) {
        var token = context.Request.Query["token"];
        if (token != "123") {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Token is invalid");
        }
        else {
            await _next.Invoke(context);
        }
    }
}
