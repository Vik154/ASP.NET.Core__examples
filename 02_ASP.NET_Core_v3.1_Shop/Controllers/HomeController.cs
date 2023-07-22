using Microsoft.AspNetCore.Mvc;
using Shop.Interfaces;
using Shop.ViewModels;

namespace Shop.Controllers {
    
    public class HomeController : Controller {

        private IAllCars _carRep;

        public HomeController(IAllCars car) {
            _carRep = car;
        }

        public ViewResult Index() {
            var homeCars = new HomeViewModel {
                favCars = _carRep.getFavCars
            };
            return View(homeCars);
        }
    }
}