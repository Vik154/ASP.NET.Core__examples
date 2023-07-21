// Логика контроллера
using Microsoft.AspNetCore.Mvc;
using Shop.Interfaces;
using Shop.ViewModels;

namespace Shop.Controllers {
   
    public class CarsController : Controller {

        private readonly IAllCars _allCars;
        private readonly ICarsCategory _carsCategory;

        public CarsController(IAllCars allCars, ICarsCategory carsCategory) { 
            _allCars = allCars;
            _carsCategory = carsCategory;
        }

        // Результат возвращается в виде html странички
        public ViewResult List() {
            CarsListViewModel model = new CarsListViewModel();
            model.AllCars = _allCars.Cars;
            model.CurrCategory = "Автомобили";
            return View(model);
        } 
    }
}