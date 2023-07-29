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
    /*
    public IActionResult DefaultCatchAll() {
        return View("Index.cshtml");
    }
    */

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

} // class HomeController