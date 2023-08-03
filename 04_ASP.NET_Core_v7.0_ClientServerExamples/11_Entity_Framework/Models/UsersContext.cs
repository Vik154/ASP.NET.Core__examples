// Класс для взаимодействия с моделями
using Microsoft.EntityFrameworkCore;
namespace _11_Entity_Framework.Models;

public class UsersContext : DbContext {
    public DbSet<User> Users { get; set; } = null;
    public DbSet<Company> Companies { get; set; } = null;
    public UsersContext(DbContextOptions<UsersContext> options) : base(options) {
        Database.EnsureCreated();
    }
}
