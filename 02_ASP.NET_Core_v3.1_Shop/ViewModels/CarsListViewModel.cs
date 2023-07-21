using Shop.Models;
using System.Collections.Generic;

namespace Shop.ViewModels {
    public class CarsListViewModel {

        public IEnumerable<Car> AllCars { get; set; }
        public string CurrCategory { get; set; }
    }
}