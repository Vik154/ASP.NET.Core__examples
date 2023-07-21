// Реализация интерфейса ICars
using Shop.Models;
using Shop.Interfaces;
using System.Collections.Generic;

namespace Shop.Mocks {

    public class MockCategory : ICarsCategory {

        public IEnumerable<Category> AllCategories {
            get {
                return new List<Category> {
                new Category {
                    categoryName = "Электромобили",
                    desc = "Современный вид транспорта" },
                new Category {
                    categoryName = "Бензиновые авто",
                    desc = "Машины с двигателем внутреннего сгорания"}
                };
            }
        }
    }
}