using Microsoft.AspNetCore.Mvc;

namespace _04_VIEW_COMPONENTS.Controllers;

public class EngineController : Controller {

    public ViewResult Index()   => View();
    public ViewResult About()   => View("About");
    public ViewResult Contact() => View();
}
