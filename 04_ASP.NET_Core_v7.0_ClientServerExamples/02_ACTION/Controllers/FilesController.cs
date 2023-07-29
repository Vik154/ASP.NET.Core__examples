using Microsoft.AspNetCore.Mvc;

namespace Action.Controllers;

public class FilesController : Controller {

    private readonly IWebHostEnvironment environment;
    public FilesController(IWebHostEnvironment env) => environment = env;

    public IActionResult Index() => View();
    
    // FileContentResult
    public IActionResult Sample1() {
        byte[] fileContent = System.IO.File.ReadAllBytes("Views/cube.jpg");
        return File(fileContent, "application/jpg", "Saved JPG File.jpg");
    }

    // FileStreamResult
    public IActionResult Sample2() {
        FileStream fileStream = System.IO.File.OpenRead("Views/cube.jpg");
        return File(fileStream, "application/jpg");
    }

    // PhysicalFileResult
    public IActionResult Sample3() {
        // environment.ContentRootPath - абсольютный путь к директории в котором хранится контент приложения.
        return PhysicalFile(environment.ContentRootPath + @"\Views\cube.jpg", "application/jpg");
    }
}