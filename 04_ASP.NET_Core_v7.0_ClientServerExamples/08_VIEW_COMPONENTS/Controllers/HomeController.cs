using Microsoft.AspNetCore.Mvc;

namespace _08_VIEW_COMPONENTS.Controllers;

public class HomeController : Controller {
    
    public IActionResult TestTimerComponent() => View();
    public IActionResult TestPersonComponent() => View();
}
