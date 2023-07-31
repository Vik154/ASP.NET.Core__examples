// Передача данных через формы в запросе POST
using Microsoft.AspNetCore.Mvc;
namespace _04_VIEW_COMPONENTS.Controllers;

public class HomeController : Controller {

    [HttpGet]
    public async Task Index() {
        string content = @"<form method='post'>
                <label>Name:</label><br />
                <input name='name' /><br />
                <label>Age:</label><br />
                <input type='number' name='age' /><br />
                <input type='submit' value='Send' />
            </form>";
        Response.ContentType = "text/html;charset=utf-8";
        await Response.WriteAsync(content);
    }

    [HttpPost]
    public string Index(string name, int age) => $"{name}: {age}";

    // Получение данных из контекста запроса
    [HttpGet]
    public async Task Index2() {
        string content = @"<form method='post' action='/Home/PersonData'>
                <label>Name:</label><br />
                <input name='name' /><br />
                <label>Age:</label><br />
                <input type='number' name='age' /><br />
                <input type='submit' value='Send' />
            </form>";
        Response.ContentType = "text/html;charset=utf-8";
        await Response.WriteAsync(content);
    }

    [HttpPost]
    public string PersonData() {
        string? name = Request.Form["name"];
        string? age = Request.Form["age"];
        return $"{name}: {age}";
    }
}
