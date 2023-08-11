// Фильтрация данных
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataFilter;

public class TestFilter {

    public static async Task ShowResult() {
        MongoClient client = new MongoClient("mongodb://localhost:27017");

        var db = client.GetDatabase("test");
        var collection = db.GetCollection<BsonDocument>("users");

        // Фильтр по которому производится поиск в БД
        var filter = new BsonDocument { { "name", "Tom" } };

        // Множественные фильтры
        // var filter = new BsonDocument { { "name", "Tom" }, { "age", 33 } };

        List<BsonDocument> find_result = await collection.Find(filter).ToListAsync();
        Console.WriteLine("Результаты поиска в БД по ключу name:tom");

        foreach (var result in find_result) Console.WriteLine(result);

        // Фильтр где все элементы не равны Tom (not equal)
        var filter2 = new BsonDocument { { "name", new BsonDocument("$ne", "Tom") } };
        List<BsonDocument> find_res = await collection.Find(filter2).ToListAsync();
        Console.WriteLine("\n\nРезультаты поиска по фильтру $ne");

        foreach (var res in find_res) Console.WriteLine(res);

        // Более сложное условие - найдем документы, где Age больше 33 и Name не равно "Tom":
        // var filter = new BsonDocument {
        //       { "age", new BsonDocument("$gt", 33) },
        //       { "name", new BsonDocument("$ne", "Tom") }
        // };
    }

    // FilterDefinitionBuilder и определение фильтров
    public static async Task FilterDef() {
        MongoClient client = new MongoClient("mongodb://localhost:27017");

        var db = client.GetDatabase("test");
        var collection = db.GetCollection<BsonDocument>("users");

        var builder = Builders<BsonDocument>.Filter;    // Определение строителя фильтров
        var filter = builder.Eq("name", "Tom");         // Поиск документов где name: Tom

        var users = await collection.Find(filter).ToListAsync();

        Console.WriteLine("Результат работы фильтра Builders:");
        foreach (var user in users) Console.WriteLine(user);

        /**********************************************************/
        // Поиск документов по age в диапозоне от 33 до 25
        var filter2 = builder.In("age", new int[] { 33, 25 });

        var users2 = await collection.Find(filter2).ToListAsync();

        Console.WriteLine("\n\nРезультат работы фильтра 2:");
        foreach (var user in users2) Console.WriteLine(user);
    }

    // Сортировка
    public static async Task SortBson() {
        MongoClient client = new MongoClient("mongodb://localhost:27017");

        var db = client.GetDatabase("test");
        var collection = db.GetCollection<BsonDocument>("person");

        var users = await collection.Find("{}").Sort("{Age:1}").ToListAsync();
        // var users = await collection.Find(new BsonDocument()).Sort(new BsonDocument("age", 1)).ToListAsync();

        foreach (var item in users) Console.WriteLine(item);

        Console.WriteLine(new string('*', 100));

        // сортировка по полю Name
        var users2 = await collection.Find("{}").SortBy(d => d["Name"]).ToListAsync();

        foreach (var user in users2) Console.WriteLine(user);
        Console.WriteLine(new string('*', 100));

        // Аналогично работает метод SortByDescending(), который сортирует по убыванию:
        // varr users = await collection.Find("{}").SortByDescending(d => d["Name"]).ToListAsync();

        // сначала сортируем по Name во возврастанию, а затем по Age по убыванию
        var sortDefinition = Builders<BsonDocument>.Sort.Ascending("Name").Descending("Age");
        var users3 = await collection.Find("{}").Sort(sortDefinition).ToListAsync();

        foreach (var user in users3) Console.WriteLine(user);
    }
}