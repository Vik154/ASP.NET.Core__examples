using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _02_RAZOR_PAGES.Pages;


public class IndexModel : PageModel {
    public string Message { get; private set; } = "";

    // public IndexModel() => Message = "Hello World";
    public string PrintTime() => DateTime.Now.ToShortTimeString();

    // Get - Запрос https://localhost:7080/?name=NAMES
    // public void OnGet(string name) => Message = $"Name: {name}";

    // GET https://localhost:7080/?name=NAMES&age=22
    // public void OnGet(string name, int age) => Message = $"Name: {name} and {age}";

    // Get - https://localhost:7080/?name=Nik&age=99
    // public void OnGet(Person person) => Message = $"Person name:{person.Name} ({person.Age}) ";

    // Get - Массив строк https://localhost:7080/?names=Mik&names=Nik&names=Pik
    //public void OnGet(string[] names) {
    //    string res = "";
    //    foreach (string name in names)
    //        res = $"{res}{name}; ";
    //    Message = res;
    //}

    // Get - передача словарей https://localhost:7080/?items[germany]=berlin&items[france]=paris&items[spain]=madrid
    public void OnGet(Dictionary<string, string> items) {
        string res = "";
        foreach (var item in items)
            res = $"{res} {item.Key} - {item.Value}; ";
        Message = res;
    }
}

public record class Person(string Name, int Age);