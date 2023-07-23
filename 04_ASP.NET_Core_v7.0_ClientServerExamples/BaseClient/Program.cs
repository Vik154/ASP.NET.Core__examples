// Пример простого клиента (работа с запросами/протоколами)
using System.Net;
using System.Net.Http.Json; // пространство имен метода GetFromJsonAsync
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BaseClient;


class Program {

    static HttpClient httpClient = new HttpClient();
    
    static async Task Main() {
        // Прмер 1, работа метода GetFromJsonAsync()
        // await ExampleGetFromJsonAsync();

        // Пример 2, работа DeleteFromJsonAsync()
        // await ExampleDeleteFromJsonAsync();

        await Example_2();
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

    // Пример 2 (На сервере)
    static async Task Example_2() {
        using var response = await httpClient.GetAsync("https://localhost:7219/1");

        if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.NotFound) {
            // получаем информацию об ошибке
            Error? error = await response.Content.ReadFromJsonAsync<Error>();
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(error?.Message);
        }
        else {
            // если запрос завершился успешно, получаем объект Person
            Person? person = await response.Content.ReadFromJsonAsync<Person>();
            Console.WriteLine($"Name: {person?.Name}   Age: {person?.Age}");
        }
    }

}

// для успешного ответа
record Person(string Name, int Age);
// для ошибок
record Error(string Message);