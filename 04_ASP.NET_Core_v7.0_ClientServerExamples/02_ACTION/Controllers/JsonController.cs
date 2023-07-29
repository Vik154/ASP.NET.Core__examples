using Microsoft.AspNetCore.Mvc;

namespace Action.Controllers;

public class JsonController : Controller {
    public IActionResult Index() {
        return View();
    }

    public IActionResult ClientInfo() {
        var client = new Client() {
            Id = 100,
            Login = "user1",
            Email = "user1@example.com"
        };

        // Json - Сериализует объект переданный в параметрах в JSON и возвращает клиенту ответ.
        return Json(client);
    }

    public IActionResult ClientInfo2() {
        // использование анонимных типов для формирования JSON ответа
        return Json(new {
            Id = 100,
            Login = "user1",
            Email = "user1@example.com"
        });
    }
}

public class Client {
    public int Id { get; set; }
    public string? Login { get; set; }
    public string? Email { get; set; }
}