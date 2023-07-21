using Microsoft.AspNetCore.Mvc;

namespace WebAppCoreV3.Areas.Admin.Controllers {
    public class ServiceItemsController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
