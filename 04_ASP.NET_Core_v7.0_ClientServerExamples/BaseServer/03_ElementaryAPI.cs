// Пример простейшего приложения Web API в стиле REST
// Источник - https://metanit.com/sharp/aspnet6/2.11.php
using System.Text.RegularExpressions;

namespace BaseServer;

// Создание сервера на ASP.NET Core, которое и будет представлять Web API
public class ElementaryAPI {

    // Начальные данные --> список объектов Person, с которыми будут работать клиенты:
    static List<APIPerson> users = new List<APIPerson>() {
        new() { Id = Guid.NewGuid().ToString(), Name = "Tom", Age = 32 },
        new() { Id = Guid.NewGuid().ToString(), Name = "Bob", Age = 32 },
        new() { Id = Guid.NewGuid().ToString(), Name = "Sam", Age = 41 }
    };

    /// <summary> Точка входа </summary>
    public static void RunApplication(string[] args) {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        WebApplication app = builder.Build();

        // Тут определяем компонент middleware, который в зависимости от типа
        // запросов (GET/POST/PUT/DELETE) выполняет те или иные действия.
        app.Run(async (context) => {
            HttpResponse? response   = context.Response;
            HttpRequest?  request    = context.Request;
            PathString    path       = request.Path;
            string expressionForGuid = @"^/api/users/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";

            // Когда приложение получает запрос типа GET по адресу "api/users",
            // то срабатывает следующий код: async Task GetAllPeople(HttpResponse response)
            if (path == "/api/users" && request.Method == "GET") {
                await GetAllPeople(response);
            }
            // Когда клиент обращается к приложению для получения одного объекта по id в запрос
            // типа GET по адресу "api/users/[id]", то срабатывает следующий код:
            // получение одного пользователя по id
            // async Task GetPerson(string? id, HttpResponse response)
            else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "GET") {
                string? id = path.Value?.Split("/")[3]; // получаем id из адреса url
                await GetPerson(id, response);
            }
            // При получении запроса с методом POST по адресу "/api/users" используем метод
            // request.ReadFromJsonAsync() для извлечения данных из запроса:
            else if (path == "/api/users" && request.Method == "POST") {
                await CreatePerson(response, request);
            }
            // Если приложению приходит PUT-запрос, то также с помощью метода
            // request.ReadFromJsonAsync() получаем отправленные клиентом данные. 
            else if (path == "/api/users" && request.Method == "PUT") {
                await UpdatePerson(response, request);
            }
            // При получении запроса DELETE действует аналогичная логика:
            else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "DELETE") {
                string? id = path.Value?.Split("/")[3];
                await DeletePerson(id, response);
            }
            // В случае, если запрос идет по другому адресу, то отправляем клиенту
            // веб-страницу elemetary_api.html
            else {
                response.ContentType = "text/html; charset=utf-8";
                await response.SendFileAsync("html/elemetary_api.html");
            }
        });

        app.Run();
    } /* void RunApplication() */

    /// <summary> получение всех пользователей </summary>
    static async Task GetAllPeople(HttpResponse response) {
        await response.WriteAsJsonAsync(users);
    }

    /// <summary> получение одного пользователя по id </summary>
    static async Task GetPerson(string? id, HttpResponse response) {
        APIPerson? user = users.FirstOrDefault((u) => u.Id == id);  // получаем пользователя по id
        
        if (user != null)
            await response.WriteAsJsonAsync(user);  // если пользователь найден, отправляем его
        else {                                      // если нет, отправляем статусный код и сообщение об ошибке
            response.StatusCode = 404;
            await response.WriteAsJsonAsync(new { message = "Пользователь не найден" });
        }
    }

    /// <summary> Удаление пользователя по id </summary>
    static async Task DeletePerson(string? id, HttpResponse response) {
        APIPerson? user = users.FirstOrDefault((u) => u.Id == id);  // получаем пользователя по id
        
        if (user != null) {                         // если пользователь найден, удаляем его
            users.Remove(user);
            await response.WriteAsJsonAsync(user);
        }
        else {                                      // если нет, отправляем статусный код и сообщение об ошибке
            response.StatusCode = 404;
            await response.WriteAsJsonAsync(new { message = "Пользователь не найден" });
        }
    }

    /// <summary> Создание пользователя </summary>
    static async Task CreatePerson(HttpResponse response, HttpRequest request) {
        try {
            var user = await request.ReadFromJsonAsync<APIPerson>();   // получаем данные пользователя
            if (user != null) {
                user.Id = Guid.NewGuid().ToString();        // устанавливаем id для нового пользователя
                users.Add(user);                            // добавляем пользователя в список
                await response.WriteAsJsonAsync(user);
            }
            else {
                throw new Exception("Некорректные данные");
            }
        }
        catch (Exception) {
            response.StatusCode = 400;
            await response.WriteAsJsonAsync(new { message = "Некорректные данные" });
        }
    }

    /// <summary> Обновление данных пользователя </summary>
    static async Task UpdatePerson(HttpResponse response, HttpRequest request) {
        try {
            APIPerson? userData = await request.ReadFromJsonAsync<APIPerson>(); // получаем данные пользователя
            if (userData != null) {
                var user = users.FirstOrDefault(u => u.Id == userData.Id);      // получаем пользователя по id
                
                if (user != null) {                         // если пользователь найден,
                    user.Age = userData.Age;                // изменяем его данные
                    user.Name = userData.Name;
                    await response.WriteAsJsonAsync(user);  // и отправляем обратно клиенту
                }
                else {
                    response.StatusCode = 404;
                    await response.WriteAsJsonAsync(new { message = "Пользователь не найден" });
                }
            }
            else {
                throw new Exception("Некорректные данные");
            }
        }
        catch (Exception) {
            response.StatusCode = 400;
            await response.WriteAsJsonAsync(new { message = "Некорректные данные" });
        }
    }

}

public class APIPerson {
    public string Id   { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age     { get; set; }
}