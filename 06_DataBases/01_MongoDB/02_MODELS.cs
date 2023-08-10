// Модели данных
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace _01_MongoDB;

public class Person {
    public ObjectId Id      { get; set; }   // Определен в MongoDB.Bson.dll.
    public string?  Name    { get; set; }
    public int      Age     { get; set; }
    public Company? Company { get; set; }
    public List<string>? Languages  { get; set; } = new List<string>();
}

public class Company {
    public string? Name { get; set; }
}

// Для запусков , чтобы main не захламлять
public class TestModel {

    public static void ShowResult() {
        Person  person  = new Person  { Name = "Tom", Age = 38 };
        Company company = new Company { Name = "Microsoft" };

        Console.WriteLine(person.ToJson());
    }

    // При создании документа мы можем воспользоваться как стандартным классом C#,
    // так и классом BsonDocument, и при необходимости перейти от одного к другому. 
    public static void ShowSwapTypes() {

        BsonDocument doc = new BsonDocument {
            {"Name","Tom"},
            {"Age", 38},
            {"Company", new BsonDocument{ {"Name" , "Microsoft"}} },
            {"Languages", new BsonArray{"english", "german", "spanish"} }
        };
        Person person = BsonSerializer.Deserialize<Person>(doc);
        Console.WriteLine(person.ToJson());

        // Обратная конвертация
        Person person2 = new Person {
            Name = "Tom",
            Age = 38,
            Company = new Company { Name = "Microsoft" },
            Languages = { "english", "german", "spanish" }
        };

        BsonDocument doc2 = person2.ToBsonDocument();
        Console.WriteLine(doc2);
    }
}