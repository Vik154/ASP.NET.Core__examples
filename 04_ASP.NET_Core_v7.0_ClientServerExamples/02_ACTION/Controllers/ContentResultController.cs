using Microsoft.AspNetCore.Mvc;

namespace Action.Controllers;

public class ContentResultController : Controller {

    public IActionResult Index()   => View();
    public IActionResult Sample1() => Content("Hello world", "text/plain");   // Ответ - Как простой текст
    public IActionResult Sample2() => Content("[1,2,3]", "application/json"); // Ответ в виде JSON контента
}