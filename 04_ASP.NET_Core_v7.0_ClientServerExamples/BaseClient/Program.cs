// Пример простого клиента (работа с запросами/протоколами)
using System.Net;
using System.Net.Http.Json; // пространство имен метода GetFromJsonAsync

namespace BaseClient;

class Program {
    // Клиент
    static HttpClient httpClient = new HttpClient();

    // Main
    static async Task Main() {
        await RunApp();
    }

    // Точка входа
    static async Task RunApp() {
        // Прмер 1, работа метода GetFromJsonAsync()
        // await ExampleGetFromJsonAsync();

        // Пример 2, работа DeleteFromJsonAsync()
        // await ExampleDeleteFromJsonAsync();

        // Пример 2 (на сервере)
        // await Example_2();

        // Пример 3 "Отправка заголовков"
        // await Example_3();

        // Пример 3.1 Установка заголовка для запроса
        // await Example_3_1();

        // Пример 3.2 Получение заголовков
        // await Example_3_2();

        // Пример 4 "Отправка строки"
        // await Example_4();

        // Пример 5 "Отправка json с помощью HttpClient"
        // await Example_5();

        // Пример 6 Взаимодействие HttpClient с Web API
        await Example_6();
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

    // Пример 3 "Отправка заголовков из консольного приложения с помощью HttpClient":
    static async Task Example_3() {
        // Адрес сервера
        var serverAddress = "https://localhost:7219";
        // Установка обоих заголовков
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla FIrefox 5.4");
        httpClient.DefaultRequestHeaders.Add("SecreteCode", "hello");

        using var response = await httpClient.GetAsync(serverAddress);
        var responseText = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseText);
    }

    // Пример 3.1 Установка заголовка для запроса
    static async Task Example_3_1() {
        // адрес сервера
        var serverAddress = "https://localhost:7219";
        using var request = new HttpRequestMessage(HttpMethod.Get, serverAddress);
        // устанавливаем оба заголовка
        request.Headers.Add("User-Agent", "Mozilla Failfox 5.6");
        request.Headers.Add("SecreteCode", "hello");

        using var response = await httpClient.SendAsync(request);
        var responseText = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseText);
    }

    // Пример 3.2 Получение заголовков
    static async Task Example_3_2() {
        var serverAddress = "https://localhost:7219";
        using var response = await httpClient.GetAsync(serverAddress);
        response.Headers.TryGetValues("date", out var dateValues);

        Console.WriteLine(dateValues?.FirstOrDefault());
    }

    // Пример 4 "Отправка строки"
    static async Task Example_4() {
        StringContent content = new StringContent("Tom");
        // определяем данные запроса
        using var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7219/data");
        // установка отправляемого содержимого
        request.Content = content;
        // отправляем запрос
        using var response = await httpClient.SendAsync(request);
        // получаем ответ
        string responseText = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseText);
    }

    // Пример 5
    static async Task Example_5() {
        // отправляемый объект 
        Person2 tom = new Person2 { Name = "Tom", Age = 38 };
        // создаем JsonContent
        JsonContent content = JsonContent.Create(tom);
        // отправляем запрос
        using var response = await httpClient.PostAsync("https://localhost:7219/create", content);
        Person2? person = await response.Content.ReadFromJsonAsync<Person2>();
        Console.WriteLine($"{person?.Id} - {person?.Name}");
    }

    // Пример 6 Взаимодействие HttpClient с Web API
    static async Task Example_6() {
        List<Person3>? people = await httpClient
            .GetFromJsonAsync<List<Person3>>("https://localhost:7219/api/users");
        if (people != null) {
            foreach (var person in people) {
                Console.WriteLine(person.Name);
            }
        }
    }
}

// для успешного ответа примеры 1 - 4
record Person(string Name, int Age);

// для ошибок
record Error(string Message);

// Пример 5
class Person2 {
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}

// Пример 6 Взаимодействие HttpClient с Web API
public class Person3 {
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}