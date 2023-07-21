// Получение всех категорий из модели Category
using Shop.Models;
using System.Collections.Generic;

namespace Shop.Interfaces {

    public interface ICarsCategory {
        IEnumerable<Category> AllCategories { get; }
    }
}