using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _02_RAZOR_PAGES.Pages;

[IgnoreAntiforgeryToken]
public class BindingPagesModel : PageModel {

    // Если в запросе будут данные с ключами name и age (регистр названий ключей не имеет значения),
    // то эти данные будут автоматически передаваться одноименным свойствам.
    [BindProperty] public string Name { get; set; } = string.Empty;
    [BindProperty] public int Age { get; set; } 
    
    // Параметры маршрута
    public int? Id { get; private set; }
    public void OnGet(int? id) => Id = id?? 0;

    // Привязка к свойствам
    [BindProperty(SupportsGet = true)] public string Info { get; set; } = "";
    [BindProperty(SupportsGet = true)] public int Num { get; set; }
}
