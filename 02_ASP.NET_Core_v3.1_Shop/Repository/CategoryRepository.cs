using Shop.DataBase;
using Shop.Interfaces;
using Shop.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Shop.Repository {
    public class CategoryRepository : IAllCars {

        private readonly AppDBContent _content;
        public CategoryRepository(AppDBContent appDB) => _content = appDB;

        public IEnumerable<Car> Cars => _content.Cars.Include((c) => c.Category);

        public IEnumerable<Car> getFavCars => _content.Cars.Where(p => p.isFavourite).Include(c => c.Category);

        public Car getObjectCar(int carId) => _content.Cars.FirstOrDefault(c => c.Id == carId);
    }
}
