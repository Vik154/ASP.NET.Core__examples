// Реализация интерфейса IAllCars
using Shop.Models;
using Shop.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Shop.Mocks {

    public class MockCars : IAllCars {

        private readonly ICarsCategory carsCategory = new MockCategory();

        public IEnumerable<Car> Cars {
            get {
                return new List<Car> {
                    new Car {
                        Name = "Tesla",
                        shortDesc = "Быстрый автомобиль",
                        longDesc = "Быстрый, тихий, экономичный",
                        img = "~/img/Tesla.jpg",
                        price = 45000,
                        isFavourite = true,
                        available = true,
                        Category = carsCategory.AllCategories.First()
                    },

                    new Car {
                        Name = "Ford fiesta",
                        shortDesc = "Тихий",
                        longDesc = "Удобный для города",
                        img = "~/img/Ford.jpg",
                        price = 11000,
                        isFavourite = false,
                        available = true,
                        Category = carsCategory.AllCategories.Last()
                    }
                };
            }
        }

        IEnumerable<Car> IAllCars.getFavCars { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Car getObjectCar(int carId) {
            throw new NotImplementedException();
        }
    }
}