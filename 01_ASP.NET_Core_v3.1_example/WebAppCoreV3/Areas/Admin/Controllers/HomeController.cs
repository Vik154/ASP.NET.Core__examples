using Microsoft.AspNetCore.Mvc;

namespace WebAppCoreV3.Areas.Admin.Controllers {

    [Area("Admin")]
    public class HomeController : Controller {
        
        public IActionResult Index() {
            return View();
        }
    }
}
