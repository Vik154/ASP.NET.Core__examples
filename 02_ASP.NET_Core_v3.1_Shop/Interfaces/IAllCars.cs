// Интерфейс для работы с товарами
using Shop.Models;
using System.Collections.Generic;

namespace Shop.Interfaces {

    public interface IAllCars {
        IEnumerable<Car> Cars { get; }
        IEnumerable<Car> getFavCars { get; set; }
        Car getObjectCar(int carId);
    }
}