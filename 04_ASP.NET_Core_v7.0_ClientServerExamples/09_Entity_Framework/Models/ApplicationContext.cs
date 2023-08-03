// Класс для взаимодействия с БД
// Чтобы взаимодействовать с базой данных через Entity Framework нам нужен контекст данных
// - класс, унаследованный от класса Microsoft.EntityFrameworkCore.DbContext.
using Microsoft.EntityFrameworkCore;
namespace _09_Entity_Framework.Models; 

public class ApplicationContext : DbContext {
    // DbSet представляет собой коллекцию объектов, которая сопоставляется с определенной таблицей в базе данны
    public DbSet<User> Users { get; set; } = null;

    // Через параметр options в конструктор контекста данных будут передаваться настройки контекста.
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
        Database.EnsureCreated(); // создаем базу данных при первом обращении
    }
}
