using Microsoft.AspNetCore.Mvc;

namespace _11_Entity_Framework.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
