using Microsoft.AspNetCore.Mvc;

namespace Action.Controllers;

public class RedirectsController : Controller {
    public IActionResult Index()   => View();
    public IActionResult Sample1() => Redirect("https://yandex.ru");    // Перенаправление 302 (временное)
    public IActionResult Sample2() => Redirect("/home/index");          // Перенаправление 302 (временное)
    public IActionResult Sample3() => RedirectPermanent("/home/index"); // Перенаправление 301 (постоянное)
    public IActionResult Sample4() => RedirectToAction("Index");        // П-ие на метод Index текущего контроллера
    public IActionResult Sample5() => RedirectToAction("Index", "Home"); // На метод Index контр-ла Home
    public IActionResult Sample6() => RedirectToRoute(new {             // Перенаправление с использованием 
        controller = "home", action = "index" });                       // значений для переменных сегментов
}