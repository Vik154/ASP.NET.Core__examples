using Shop.DataBase;
using Shop.Interfaces;
using Shop.Models;
using System.Collections.Generic;

namespace Shop.Repository {
    public class CategoryRepository : ICarsCategory {

        private readonly AppDBContent _content;
        public CategoryRepository(AppDBContent appDB) => _content = appDB;

        public IEnumerable<Category> AllCategories => _content.Categories;
    }
}