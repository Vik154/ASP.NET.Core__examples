using _10_Entity_Framework.Models;
using Microsoft.EntityFrameworkCore;

namespace _10_Entity_Framework;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        string connect = "Server = (localdb)\\mssqllocaldb;Database = userstoredb;Trusted_Connection=true";
        builder.Services.AddDbContext<UsersContext>(opt => opt.UseSqlServer(connect));
        builder.Services.AddControllersWithViews();
        var app = builder.Build();
        app.MapDefaultControllerRoute();
        app.Run();
    }
}