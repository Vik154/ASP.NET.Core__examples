using Microsoft.AspNetCore.Mvc;
namespace _04_VIEW_COMPONENTS.Areas.Account.Controllers;

// Без этого атрибута контроллер не будет принадлежать области Account
[Area("Acc")]
public class HomeController : Controller {

    [Route("{area}")]
    [Route("{area}/{controller}")]
    [Route("{area}/{controller}/{action}")]
    public IActionResult Index() => View("Index");
    public string Info() => "From Area HomeController";
}
