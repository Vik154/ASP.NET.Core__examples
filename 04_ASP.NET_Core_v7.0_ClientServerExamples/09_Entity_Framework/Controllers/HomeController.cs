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

    // Удаление объекта (метод Delete обрабатывает только запросы типа POST)
    // с помощью параметра id получаем удаляемый объект из БД
    [HttpPost]
    public async Task<IActionResult> Delete(int? id) {
        if (id != null) {
            User? user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null) {
                db.Users.Remove(user);              // Данный метод генерирует sql-выражение DELETE
                await db.SaveChangesAsync();        // выполняется вызовом db.SaveChangesAsync()
                return RedirectToAction("Index");
            }
        }
        return NotFound();
    }

    // Редактирование объектов
    // GET-версия метода Edit возвращает форму с данными объекта, которые пользователь может отредактировать
    public async Task<IActionResult> Edit(int? id) {
        if (id != null) {
            User? user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
                return View("user");
        }
        return NotFound();
    }

    // А post-версия Edit получает отредактированные данные в виде объекта user и с помощью метода
    // db.Users.Update(user) для этих данных будет генерироваться sql-выражение UPDATE,
    // которое будет выполнено вызовом db.SaveChangesAsync()
    [HttpPost]
    public async Task<IActionResult> Edit(User user) {
        db.Users.Update(user);
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
