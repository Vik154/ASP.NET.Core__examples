using Microsoft.AspNetCore.Mvc;
namespace _07_TAGHelpers.Controllers;

public class HomeController : Controller {

    public IActionResult Index() => View();
    public IActionResult About() => View();
}
