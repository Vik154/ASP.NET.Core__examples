﻿using Microsoft.AspNetCore.Mvc;

namespace WebAppCoreV3.Controllers {

    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
