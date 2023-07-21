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
                        img = "https://yandex.ru/images/search?from=tabbar&img_url=https%3A%2F%2Fscontent-hel3-1.cdninstagram.com%2Fv%2Ft51.2885-15%2Fe35%2F228239427_513374559945586_3814176478460614389_n.jpg%3F_nc_ht%3Dscontent-hel3-1.cdninstagram.com%26_nc_cat%3D110%26_nc_ohc%3Dvh_mP5-0Nj0AX_cuck5%26edm%3DABfd0MgBAAAA%26ccb%3D7-4%26oh%3Da1c452684d81ffa9fac9b034f6623bd8%26oe%3D610FCB53%26_nc_sid%3D7bff83&lr=65&pos=0&rpt=simage&text=tesla",
                        price = 45000,
                        isFavourite = true,
                        available = true,
                        Category = carsCategory.AllCategories.First()
                    },

                    new Car {
                        Name = "Ford fiesta",
                        shortDesc = "Тихий",
                        longDesc = "Удобный для города",
                        img = "https://yandex.ru/images/search?from=tabbar&img_url=https%3A%2F%2Fwebpulse.imgsmail.ru%2Fimgpreview%3Fmb%3Dwebpulse%26key%3Dpulse_cabinet-image-848d95eb-b290-4bb5-8fa3-152ade712001&lr=65&pos=0&rpt=simage&text=ford%20fiesta",
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