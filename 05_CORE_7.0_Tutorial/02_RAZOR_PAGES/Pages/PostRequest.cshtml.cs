using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _02_RAZOR_PAGES.Pages;

// Ётот атрибут отключает валидацию формы, потому что модель Razor
// дл€ валидации полученных форм использует специальный токен - AntiforgeryToken.
[IgnoreAntiforgeryToken]
public class PostRequestModel : PageModel {

    public string Message { get; private set; } = "";

    public void OnGet() => Message = "¬ведите им€: ";
    public void OnPost(string name) => Message = $"ѕривет {name}!";    
}
