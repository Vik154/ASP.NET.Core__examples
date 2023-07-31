using Microsoft.AspNetCore.Mvc;

namespace _06_HTMLHellpers.Controllers;

public class ListHelperController : Controller {
    
    public IActionResult Index() => View("ListHelper");
}
