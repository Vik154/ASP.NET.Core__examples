// Проекция - используется для получения совершенно других данные
// из изначальных документов коллекции
using MongoDB.Bson;
using MongoDB.Driver;

namespace Projection;

public class TestProj {

    // Построение проекции
    public static async Task ShowRes() {
        MongoClient client = new MongoClient("mongodb://localhost:27017");

        var db = client.GetDatabase("test");
        var collection = db.GetCollection<BsonDocument>("users");

        var projection = Builders<BsonDocument>
            .Projection
            .Include("Name")
            .Include("name")
            .Include("Age")
            .Include("age")
            .Exclude("_id");

        var users = await collection.Find("{}").Project(projection).ToListAsync();

        // Можно и так
        // var users = await collection.Find("{}").Project("{Name:1, Age:1, _id:0}").ToListAsync();

        foreach (var user in users) Console.WriteLine(user);
    }

    // Группировка
    public static async Task ShowGroup() {
        MongoClient client = new MongoClient("mongodb://localhost:27017");

        var db = client.GetDatabase("test");

        // Если единожды создал - исключение генерит
        // db.CreateCollection("employes");

        var collection = db.GetCollection<BsonDocument>("employes");

        if (collection.CountDocuments("{}") < 1) {
            await collection.InsertManyAsync(new List<BsonDocument> {
                new BsonDocument { {"Name", "Tom"}, {"Age", 38}, {"Company", new BsonDocument { {"Title", "Microsoft"} } } },
                new BsonDocument { {"Name", "Bob"}, {"Age", 34}, {"Company", new BsonDocument { {"Title", "Google"} } } },
                new BsonDocument { {"Name", "Sam"}, {"Age", 33}, {"Company", new BsonDocument { {"Title", "Microsoft"} } } },
                new BsonDocument { {"Name", "Tim"}, {"Age", 39}, {"Company", new BsonDocument { {"Title", "Google"} } } },
                new BsonDocument { {"Name", "Dan"}, {"Age", 31}, {"Company", new BsonDocument { {"Title", "Google" } } } }
            });
        }

        var find = await collection.Find("{}").ToListAsync();
        foreach (var doc in find) { Console.WriteLine(doc); }

        /*--------------------------------------------------------------*/
        Console.WriteLine(new string('*', 100) + "\nПример группировки:");
        
        var employes = await collection.Aggregate()
            .Group(new BsonDocument {
                { "_id", "$Company.Title" },
                { "count", new BsonDocument("$sum", 1) }
            })
            .ToListAsync();

        foreach (var employeeGroup in employes)
            Console.WriteLine(employeeGroup);
        
        /*--------------------------------------------------------------*/
        Console.WriteLine(new string('*', 100) + "\nПример среднего возраста:");

        var employees = await collection.Aggregate()
            .Group(new BsonDocument {
                { "_id", "$Company.Title" },
                { "minAge", new BsonDocument("$min", "$Age") },
                { "maxAge", new BsonDocument("$max", "$Age") },
                { "avgAge", new BsonDocument("$avg", "$Age") }
            })
            .ToListAsync();

        foreach (var employeeGroup in employees)
            Console.WriteLine(employeeGroup);
    }
}