// Класс для работы с БД
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shop.Interfaces;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.DataBase {

    public class DBObjects {

        private static Dictionary<string, Category> category;
    
        public static void Initial(AppDBContent content) {
            
            if (!content.Categories.Any())
                content.Categories.AddRange(Categories.Select(c => c.Value));
            
            if (!content.Cars.Any()) {
                content.AddRange(
                    new Car {
                        Name = "Tesla",
                        shortDesc = "Быстрый автомобиль",
                        longDesc = "Быстрый, тихий, экономичный",
                        img = "/img/Tesla.jpg",
                        price = 45000,
                        isFavourite = true,
                        available = true,
                        Category = Categories["Электромобили"]
                    },

                    new Car {
                        Name = "Ford fiesta",
                        shortDesc = "Тихий",
                        longDesc = "Удобный для города",
                        img = "/img/Ford.jpg",
                        price = 11000,
                        isFavourite = false,
                        available = true,
                        Category = Categories["Бензиновые авто"]
                    }
                );
            }
            content.SaveChanges();
        }

        public static Dictionary<string, Category> Categories {
            get {
                if (category == null) {
                    var list = new Category[] {
                        new Category {
                            categoryName = "Электромобили",
                            desc = "Современный вид транспорта"
                        },
                        new Category {
                            categoryName = "Бензиновые авто",
                            desc = "Машины с двигателем внутреннего сгорания"
                        }
                    };
                    category = new Dictionary<string, Category>();
                    foreach (Category elem in list) {
                        category.Add(elem.categoryName, elem);
                    }
                }
                return category;
            }
        }
    }
}