// Пример маршрутизации на основе атрибутов
using Microsoft.AspNetCore.Mvc;
namespace _04_VIEW_COMPONENTS.Controllers;

[Route("API")]
public class RoutingController : Controller {

    [Route("Routing/Index")]
    public IActionResult Index() => Content("From RoutingController; method: Index");

    [Route("About")]
    public IActionResult About() => Content("From Routing method About");

    [Route("frfr")]
    public IActionResult Frfr() => Content("Fr fr fr fr");

    [Route("{name:minlength(3)}/{age:int}")]
    public string Person(string name, int age) => $"Name: {name}; age: {age}";

    [Route("Path")]
    [Route("{controller}/{action}")]
    public string RoutePath() {
        var controller = RouteData.Values["controller"];
        var action = RouteData.Values["action"];
        return $"controller: {controller} <---> action: {action}";
    }
}
