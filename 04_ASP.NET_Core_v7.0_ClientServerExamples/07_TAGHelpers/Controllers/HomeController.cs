using _07_TAGHelpers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _07_TAGHelpers.Controllers;

public class HomeController : Controller {

    public IActionResult Index() => View();
    public IActionResult About() => View();

    // Tag-хелперы форм
    IEnumerable<Company> companies = new List<Company> {
        new Company( 1, "Apple"),
        new Company(2, "Samsung"),
        new Company(3, "Google")
    };

    public IActionResult Create() {
        ViewBag.Companies = new SelectList(companies, "Id", "Name");
        return View();
    }

    [HttpPost]
    public string Create(Product product) {
        Company company = companies.FirstOrDefault(c => c.Id == product.CompanyId);
        return $"Добавлен новый элемент: {product.Name} ({company?.Name})";
    }
}
