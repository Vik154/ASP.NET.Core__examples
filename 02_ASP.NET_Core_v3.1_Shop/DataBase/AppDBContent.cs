// Класс для работы с базой данных
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.DataBase {
    
    public class AppDBContent : DbContext {
        public AppDBContent(DbContextOptions<AppDBContent> options) 
            : base(options) { }            
    
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShopCartItem> ShopCartItem { get; set; }
    }
}
