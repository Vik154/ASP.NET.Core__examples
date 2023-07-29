using LtModel;
using Microsoft.AspNetCore.Mvc;

namespace Layout.Controllers;

public class HomeController : Controller {
    public IActionResult Index()    => View(model);
    public IActionResult Catalog()  => View();
    public IActionResult Contacts() => View(model);

    ContactsViewModel model;
    public HomeController() {
        model = new ContactsViewModel() {
            Email = "user@mail.com",
            Phone = "+9 999 8888 99 98",
            Address = "Str. abcd 20"
        };
    }
}
