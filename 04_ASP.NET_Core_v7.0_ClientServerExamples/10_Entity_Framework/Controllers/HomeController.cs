using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _10_Entity_Framework.Models;
namespace _10_Entity_Framework.Controllers;

public class HomeController : Controller {

    UsersContext db;
    public HomeController(UsersContext context) {
        db = context;

        // добавим начальные данные для тестирования
        if (!db.Companies.Any()) {
            Company oracle    = new Company { Name = "Oracle" };
            Company google    = new Company { Name = "Google" };
            Company microsoft = new Company { Name = "Microsoft" };
            Company apple     = new Company { Name = "Apple" };

            User user1 = new User { Name = "Олег Васильев",   company = oracle,     Age = 26 };
            User user2 = new User { Name = "Александр Овсов", company = oracle,     Age = 24 };
            User user3 = new User { Name = "Алексей Петров",  company = microsoft,  Age = 25 };
            User user4 = new User { Name = "Иван Иванов",     company = microsoft,  Age = 26 };
            User user5 = new User { Name = "Петр Андреев",    company = microsoft,  Age = 23 };
            User user6 = new User { Name = "Василий Иванов",  company = google,     Age = 23 };
            User user7 = new User { Name = "Олег Кузнецов",   company = google,     Age = 25 };
            User user8 = new User { Name = "Андрей Петров",   company = apple,      Age = 24 };

            db.Companies.AddRange(oracle, microsoft, google, apple);
            db.Users.AddRange(user1, user2 , user3, user4, user5, user6, user7, user8);
            db.SaveChanges();
        }        
    } // ctor

    public async Task<IActionResult> Index(SortState sortOrder = SortState.NameAsc) {
        IQueryable<User>? users = db.Users.Include(x => x.company);

        ViewData["NameSort"] = sortOrder == SortState.NameAsc    ? SortState.NameDesc    : SortState.NameAsc;
        ViewData["AgeSort"]  = sortOrder == SortState.AgeAsc     ? SortState.AgeDesc     : SortState.AgeAsc;
        ViewData["CompSort"] = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;

        users = sortOrder switch {
            SortState.NameDesc    => users.OrderByDescending(s => s.Name),
            SortState.AgeAsc      => users.OrderBy(s => s.Age),
            SortState.AgeDesc     => users.OrderByDescending(s => s.Age),
            SortState.CompanyAsc  => users.OrderBy(s => s.company!.Name),
            SortState.CompanyDesc => users.OrderByDescending(s => s.company!.Name),
            _ => users.OrderBy(s => s.Name),
        };
        return View(await users.AsNoTracking().ToListAsync());
    }

    // Сортировка на основе тег-хелпера
    public async Task<IActionResult> THIndex(SortState sortOrder = SortState.NameAsc) {
        IQueryable<User> users = db.Users.Include(x => x.company);

        users = sortOrder switch {
            SortState.NameDesc    => users.OrderByDescending(s => s.Name),
            SortState.AgeAsc      => users.OrderBy(s => s.Age),
            SortState.AgeDesc     => users.OrderByDescending(s => s.Age),
            SortState.CompanyAsc  => users.OrderBy(s => s.company!.Name),
            SortState.CompanyDesc => users.OrderByDescending(s => s.company!.Name),
            _ => users.OrderBy(s => s.Name),
        };
        IndexViewModel viewModel = new IndexViewModel {
            Users = await users.AsNoTracking().ToListAsync(),
            SortViewModel = new SortViewModel(sortOrder)
        };
        return View(viewModel);
    }
}
