using Microsoft.AspNetCore.Mvc;

namespace Action.Controllers;

public class StatusCodesController : Controller {   
    public IActionResult Index()   => View();
    public IActionResult Sample1() => StatusCode(200);  // Возвращение указанного статус кода
    public IActionResult Sample2() => Ok();             // 200
    public IActionResult Sample3() => Created(
        Request.Path + "/123",                          // Создание на стороне сервера объектов (адрес объекта)
        new { prop1 = "A", prop2 = "B"});               // 201 подтверждение успешного создания (сам объект)
    public IActionResult Sample4() => BadRequest();     // 400 Неправильно составленный запрос на сервер
    public IActionResult Sample5() => Unauthorized();   // 401 Недостаточно прав у пользователя при запросе
    public IActionResult Sample6() => NotFound();       // 404 Нормальная девушка
}