using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Routing.Controllers {

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

        // GET: HomeController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}
