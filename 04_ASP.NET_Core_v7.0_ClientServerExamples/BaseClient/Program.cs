// Пример простого клиента (работа с запросами/протоколами)
using System.Net.Http.Json; // пространство имен метода GetFromJsonAsync

namespace BaseClient;


class Program {

    static HttpClient httpClient = new HttpClient();
    
    static async Task Main() {
        // Прмер 1, работа метода GetFromJsonAsync()
        await ExampleGetFromJsonAsync();

        // Пример 2, работа DeleteFromJsonAsync()
        await ExampleDeleteFromJsonAsync();
    }

    // EX-1.0 GetFromJsonAsync() - отправляет запрос GET и возвращает десериализованные объекты из JSON
    static async Task ExampleGetFromJsonAsync() {
        object? data = await httpClient.GetFromJsonAsync("https://localhost:7219/", typeof(Person));
        if (data is Person person) {
            Console.WriteLine($"Name: {person.Name}   Age: {person.Age}");
        }
    }

    // EX-2.0 DeleteFromJsonAsync() - отправляет запрос DELETE и возвращает десериализованные объекты из JSON
    static async Task ExampleDeleteFromJsonAsync() {
        Person? person = await httpClient.GetFromJsonAsync<Person>("https://localhost:7219/");
        Console.WriteLine($"Name: {person?.Name}   Age: {person?.Age}");
    }

}

record Person(string Name, int Age);