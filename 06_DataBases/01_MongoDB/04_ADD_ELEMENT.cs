// Добавление элементов в бд
using MongoDB.Bson;
using MongoDB.Driver;

namespace AddElement;

// Класс для запусков
public class TestElem {

    // Добавление документов в БД
    public static async Task ShowResult() {
        MongoClient client = new MongoClient("mongodb://localhost:27017");  // Подключение к бд

        var db = client.GetDatabase("test");                    // Получение бд
        var users = db.GetCollection<BsonDocument>("users");    // Получение коллекции users

        BsonDocument addElem = new BsonDocument {
            { "Name", "Lie" },
            { "Age", 35 }
        };

        await users.InsertOneAsync(addElem);

        Console.WriteLine($"Добавлен элемент: {addElem}");

        // Добаление множественное
        BsonDocument bob = new BsonDocument { { "Name", "Bob" }, { "Age", 27 } };
        BsonDocument sam = new BsonDocument { { "Name", "Sam" }, { "Age", 27 } };

        await users.InsertManyAsync(new List<BsonDocument> { bob, sam });

        Console.WriteLine($"Добавлен элемент: {bob}");
        Console.WriteLine($"Добавлен элемент: {sam}");
    }

    // Получение документов из БД
    public static async Task GetDoc() {
        MongoClient client = new MongoClient("mongodb://localhost:27017");

        var db = client.GetDatabase("test");                        // Получение базы данных test
        var collection = db.GetCollection<BsonDocument>("users");   // Получение коллекции users

        List<BsonDocument> allUsers = await collection.Find(new BsonDocument()).ToListAsync();
        // var allUsers = await collection.Find("{}").ToListAsync();
        Console.WriteLine("Список всех записей в users:");

        foreach (var item in allUsers)
            Console.WriteLine(item);
        Console.WriteLine(new string('*', 50));

        // Метод FindAsync() предоставляет результат в виде объекта IAsyncCursor,
        // который представляет собой курсор, который осуществляет перебор объектов.
        // Применив к нему методы ToList()/ToListAsync() также можно получить список объектов:
        using var cursor = await collection.FindAsync(new BsonDocument());  // Получение курсора
        List<BsonDocument> users = cursor.ToList();                         // Получение из курсора данных

        foreach (var item in users)
            Console.WriteLine(item);
    }

    // Получение данных в виде POCO С# классов
    public static async Task GetCSClass() {
        MongoClient client = new MongoClient("mongodb://localhost:27017");

        var db = client.GetDatabase("test");                                // получаем базу данных test
        db.CreateCollection("person");                                      // Создание коллекции person
        var collection = db.GetCollection<Person>("person");                // получаем из бд коллекцию person

        await collection.InsertManyAsync(new List<Person> {                 // Добавление элементов в коллекцию
            new Person {Age = 25, Name = "Боб"},
            new Person {Age = 33, Name = "Том"},    
            new Person {Age = 37, Name = "Тим"}    
        });

        using var cursor = await collection.FindAsync(new BsonDocument());  // получаем курсор
        List<Person> users = cursor.ToList();                               // из курсора получаем список данных

        foreach (var user in users)
            Console.WriteLine($"{user.Name} - {user.Age}");
    }
}

// атрибут [BsonIgnoreExtraElements] - поволяет игнорировать все ненужные поля документов:
// т.е. не будет сгенерировано исключение при несоответсвии полей класса и БД
class Person {
    public ObjectId Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}