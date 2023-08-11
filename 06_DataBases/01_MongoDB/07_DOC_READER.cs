// Редактирование документов
using MongoDB.Bson;
using MongoDB.Driver;

namespace DocReader;

public class TestReader {

    public static async Task ShowResult() {
        MongoClient client = new MongoClient("mongodb://localhost:27017");

        var db = client.GetDatabase("test");
        var collection = db.GetCollection<BsonDocument>("users");

        var filter = new BsonDocument { { "name", "Tom" }, { "age", 28 } };     // Искомый в БД документ
        var newDoc = new BsonDocument { { "Name", "Tomas" }, { "Age", 44 } };   // Заменяемый
        var result = await collection.ReplaceOneAsync(filter, newDoc);          // Замена Tom на Tomas

        Console.WriteLine($"Найдено по соответствию: {result.MatchedCount}; обновлено: {result.ModifiedCount}");

        var filter2 = new BsonDocument { { "name", "Bob" }, { "age", 99 } };
        var newDoc2 = new BsonDocument { { "Name", "Bim" }, { "Age", 36 } };
        // выполняем замену, если документ не найден, то вставку
        var result2 = await collection.ReplaceOneAsync(filter2, newDoc2, new ReplaceOptions { IsUpsert = true });

        Console.WriteLine($"Найдено по соответствию: {result2.MatchedCount}; обновлено: {result2.ModifiedCount}");

        // проверяем - выводи документы после обновления
        var users = await collection.Find("{}").ToListAsync();
        foreach (var user in users) Console.WriteLine(user);
    }

    // Обновление отдельных полей докумета
    public static async Task ShowUpdate() {
        MongoClient client = new MongoClient("mongodb://localhost:27017");
        var db = client.GetDatabase("test");
        var collection = db.GetCollection<BsonDocument>("users");

        var filter = new BsonDocument { { "name", "Tom" } };
        var update = new BsonDocument { { "$set", new BsonDocument("Age", 39) } };

        var result = await collection.UpdateOneAsync(filter, update);


        Console.WriteLine($"Matched: {result.MatchedCount}; Modified: {result.ModifiedCount}");

        var users = await collection.Find("{}").ToListAsync();
        foreach (var user in users) Console.WriteLine(user);
    }
}