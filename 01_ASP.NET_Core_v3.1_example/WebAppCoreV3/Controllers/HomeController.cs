using Microsoft.AspNetCore.Mvc;
using WebAppCoreV3.Domain;

namespace WebAppCoreV3.Controllers {

    public class HomeController : Controller {
        private readonly DataManager dataManager;

        public HomeController(DataManager dataManager) {
            this.dataManager = dataManager;
        }

        public IActionResult Index() {
            return View(dataManager.TextFields.GetTextFieldByCodeWord("PageIndex"));
        }

        public IActionResult Contacts() {
            return View(dataManager.TextFields.GetTextFieldByCodeWord("PageContacts"));
        }
    }
}