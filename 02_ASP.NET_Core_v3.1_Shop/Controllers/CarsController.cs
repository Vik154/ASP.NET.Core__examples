// Логика контроллера
using Microsoft.AspNetCore.Mvc;
using Shop.Interfaces;

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
            var cars = _allCars.Cars;
            return View(cars);
        } 
    }
}