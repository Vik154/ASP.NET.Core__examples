using Microsoft.AspNetCore.Mvc;

namespace Action.Controllers;

public class HomeController : Controller {
    
    public IActionResult Index() => View();
}