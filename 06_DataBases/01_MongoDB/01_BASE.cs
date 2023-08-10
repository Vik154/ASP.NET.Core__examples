// Базовые концепции MongoDB
using MongoDB.Bson;
using MongoDB.Driver;

namespace _01_MongoDB;

internal class _01_BASE {

    public static async Task ShowResult() {

        // Подключение к серверу базы данных
        MongoClient client = new MongoClient("mongodb://localhost:27017");

        // Получение базы данных
        var db = client.GetDatabase("test");

        // Создание коллекции people
        // Если попытаться создать коллекцию, которая уже существует, то приложение сгенерирует исключение
        // await db.CreateCollectionAsync("people");

        // Получение списка коллекций
        var collections = await db.ListCollectionsAsync();

        foreach (var collection in collections.ToList())
            Console.WriteLine(collection);

        Console.WriteLine(new string('*', 50));

        // только имена коллекций
        var collectionsName = await db.ListCollectionNamesAsync();
        foreach (var collection in collectionsName.ToList())
            Console.WriteLine(collection);

        Console.WriteLine(new string('*', 50));

        // Создание и получение коллекции
        db.CreateCollectionAsync("newusers");

        IMongoCollection<BsonDocument> newusers = db.GetCollection<BsonDocument>("newusers");

        // Создание документов и элементов bson
        BsonElement bsonElement = new BsonElement("name", "Tor");
        BsonDocument bsonDoc = new BsonDocument(bsonElement);
        Console.WriteLine($"BSON Element: {bsonDoc}");

        // Сложные композиции документов
        BsonDocument doc = new BsonDocument {
            { "name", "Tom" },
            { "age", 38 },
            { "company", new BsonDocument{{"name", "Microsoft"}} },
            { "languages", new BsonArray{"English", "German", "Spanish"} }
        };
        Console.WriteLine($"BSON DOC: {doc}");

        // Обращение к полям документа
        Console.WriteLine($"DOC[\"name\"] = {doc["name"]}");
        Console.WriteLine($"DOC[\"lang\"] = {doc["languages"]}");
    }
}