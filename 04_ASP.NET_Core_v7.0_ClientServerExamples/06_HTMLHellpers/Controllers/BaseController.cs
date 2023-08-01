using Microsoft.AspNetCore.Mvc;

namespace _06_HTMLHellpers.Controllers;

public class BaseController : Controller {

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public string Create(string name, int age) => $"{name} - {age}";
}
