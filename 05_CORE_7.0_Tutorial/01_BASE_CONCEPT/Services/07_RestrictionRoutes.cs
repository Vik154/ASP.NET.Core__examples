// Конечные точки марщрутов
// Ограничения маршрутов
namespace _01_BASE_CONCEPT.Services;

/* IEndpointRouteBuilder.Map() - добавляет конечные точки для обработки запросов типа GET. Данный метод имеет три версии: 
 * 
public static RouteHandlerBuilder Map (this IEndpointRouteBuilder endpoints, RoutePattern pattern, Delegate handler);
public static IEndpointConventionBuilder Map (this IEndpointRouteBuilder endpoints, string pattern, RequestDelegate requestDelegate);
public static RouteHandlerBuilder Map (this IEndpointRouteBuilder endpoints, string pattern, Delegate handler);
*/

public static class MyMaps {

    private static WebApplication webApp;

    public static void SetMaps(ref WebApplication web) => webApp = web;

    public static void ShowMaps() {
        webApp.Map("/", () => "Index page");
        webApp.Map("/about", () => "About page");

        // Параметры маршрута
        webApp.Map("/user/{id?}", (string? id) => $"User ID: {id}");
        webApp.Map("/users/{id?}/{name?}", 
            (string? id, string? name) => $"ID: {id??"not id"}; Name: {name??"no name"}");

        // Ограничения маршрутов
        webApp.Map("/RestrictInt/{id:int}", (int id) => $"Rt INT: {id}");
        webApp.Map("/RestrictStr/{str:length(3)}", (string str) => $"Rt STR: {str}");
    }

}

// Создания собственного ограничения маршрута IRouteConstraint
public interface IRouteConstraint {

    // true, если зпрос удовлетворяет данному ограничению маршрута, и false, если не удовлетворяет.
    bool Match(HttpContext context,             // инкапсулирует информацию о HTTP-запросе
               IRouter? router,                 // представляет маршрут, в рамках которого применяется ограничение
               string routeKey,                 // название параметра маршрута, к которому применяется ограничение
               RouteValueDictionary values,     // набор параметров маршрута в виде словаря, где ключи - названия параметров, а значения - значения параметров маршрута
               RouteDirection routeDirection);  // применяется ограничение при обработке запроса, либо при генерации ссылки
}

// класс ограничения маршрута
public class SecretCodeConstraint : IRouteConstraint {

    string secretCode;

    // Через конструктор принимает некий условно секретный код, которому должен соответствовать параметр маршрута
    public SecretCodeConstraint(string secretCode_) => secretCode = secretCode_;

    public bool Match(HttpContext context, IRouter? router, string routeKey,
                      RouteValueDictionary values, RouteDirection routeDirection) 
    {
        // values[routeKey] получаем из словаря values значение параметра маршрута,
        // имя которого передается через routeKey. И затем это значение сравниванием с секретным кодом:
        return values[routeKey]?.ToString() == secretCode;
    }
}