using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Routing.Controllers; 

public class HomeController : Controller {

    // GET: HomeController; View: Idex
    public ActionResult Index() {
        return View();
    }

    // View: List
    public IActionResult List() {
        string[] list = { "item 1", "item 2", "item 3" };
        return View(list);
    }

    // Пример маршрута с параметрами. Запрос в строке браузера:
    // https://localhost:7253/home/sumres/2/5
    public int SumRes(int x, int y) {
        return x + y;
    }

    // Пример маршрута с параметрами, возвращающее представление View
    // Запрос в строке браузера: https://localhost:7253/home/sumresult/2/5
    public IActionResult SumResult(int x, int y) {
        int res = x + y;
        return View(res);
    }

    // Прмер catch-all параметров (когда неизвестно колво сегментов url:home/value/xxxx/xxx/xx
    public IActionResult Values(string? data) {

        if (data == null)
            return View("Index");

        string[] splited = data.Split("/");
        int result = 0;

        foreach (string item in splited) {
            if (int.TryParse(item, out int converted)) {
                result += converted;
            }
        }
        
        return View(result);
    }

    // Пример генерации url-адресов
    // Свойство Url полученное по наследству от базового класса Controller содержит IUrlHelper,
    // который содержит методы для построение URL в MVC приложениях.
    public IActionResult GenerateURL() {
        string? url1 = Url.Action("Item");                           // создание URL по имени метода действия
        string? url2 = Url.Action("Item", new { id = 1 });           // создание URL по имени метода действия с указанием параметра id
        string? url3 = Url.Action("Index", "Test");                  // создание URL по имени метода действия и контроллера
        string? url4 = Url.Action("List", "Test", new { id = 1 });   // URL по имени метода, контроллера и указания параметров

        string?[] model = { url1, url2, url3, url4 };

        return View(model);
    }

    public string Item() {
        return "Item";
    }

} // class HomeController