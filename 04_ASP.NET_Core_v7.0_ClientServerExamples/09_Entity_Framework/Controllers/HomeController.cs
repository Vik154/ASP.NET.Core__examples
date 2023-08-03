// Стандартный контроллер
// Получение контекста данных в контроллере
using _09_Entity_Framework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _09_Entity_Framework.Controllers;


public class HomeController : Controller {

    // Для взаимодействия с базой данных определяется переменная ApplicationContext db.
    ApplicationContext db;

    // получить переданный контекст данных
    public HomeController(ApplicationContext context) => db = context;

    // Получение объектов из бд, создание из них списка и передача в представление.
    public async Task<IActionResult> Index() => View(await db.Users.ToListAsync());

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(User user) {
        db.Users.Add(user);                 // для данных из объекта user формируется sql-выражение INSERT
        await db.SaveChangesAsync();        // выполняет это выражение, тем самым добавляя данные в базу данных
        return RedirectToAction("Index");
    }
}
