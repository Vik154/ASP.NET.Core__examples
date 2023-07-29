using Microsoft.AspNetCore.Mvc;

namespace Routing.Controllers;

public class TestController : Controller {
    public IActionResult Index() => View();
    public IActionResult List() => View();
}