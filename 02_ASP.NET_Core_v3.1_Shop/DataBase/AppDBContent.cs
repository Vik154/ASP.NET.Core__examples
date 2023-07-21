using Microsoft.EntityFrameworkCore;

namespace Shop.DataBase {
    
    public class AppDBContent : DbContext {
        public AppDBContent(DbContextOptions<AppDBContent> options) 
            : base(options) { }            
    }
}
