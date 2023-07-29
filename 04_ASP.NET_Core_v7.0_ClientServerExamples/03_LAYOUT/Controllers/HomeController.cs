using Microsoft.AspNetCore.Mvc;

namespace Layout.Controllers;

public class HomeController : Controller {
    public IActionResult Index()    => View();
    public IActionResult Catalog()  => View();
    public IActionResult Contacts() => View();
}
