using Microsoft.AspNetCore.Mvc;
using WebAppCoreV3.Domain;

namespace WebAppCoreV3.Areas.Admin.Controllers {

    [Area("Admin")]
    public class HomeController : Controller {

        private readonly DataManager _dataManager;
        public HomeController(DataManager dataManager) => _dataManager = dataManager;
        
        public IActionResult Index() {
            return View(_dataManager.ServiceItems.GetServiceItems());
        }
    }
}
