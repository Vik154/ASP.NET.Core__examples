using _05_MODELS.Models;
using Microsoft.AspNetCore.Mvc;
namespace _05_MODELS.Controllers;

public class EventController : Controller {

    static List<Event> events = new List<Event>();
    
    public IActionResult Index()  => View(events);
    public IActionResult Create() => View();
    
    [HttpPost]
    public IActionResult Create(Event myEvent) {
        myEvent.Id = Guid.NewGuid().ToString();
        events.Add(myEvent);
        return RedirectToAction("Index");
    }
}
