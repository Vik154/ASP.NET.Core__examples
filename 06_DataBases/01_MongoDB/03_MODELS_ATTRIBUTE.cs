// Настройка модели с помощью атрибутов
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace AttrModels;

public class Person {

    // Каждый объект в базе данных имеет поле _id, которое выполняет роль уникального идентификатора объекта.
    // Используя атрибут BsonId мы можем явно установить свойство, которое будет выполнять роль идентификатора:
    // при создании документа данное свойство будет представлять в документе поле _id
    [BsonId] 
    public int PersonId { get; set; }

    public string Name { get; set; } = "";

    // Атрибут BsonIgnore позволяет не учитывать свойство при сериализации объекта в документ
    [BsonIgnore]
    public string Email { get; set; } = "";

    [BsonIgnoreIfDefault]
    public int Age { get; set; }

    [BsonIgnoreIfNull]
    public Company? Company { get; set; }
}

public class Company {
    public string Name { get; set; } = "";
}

public class TestAttr {

    public static void ShowResult() {
        // исключается из сериализации в BsonDocument свойство Email.
        Person person = new Person { Name = "Tim", Email = "Tim@mail" };
        Console.WriteLine(person.ToBsonDocument());
    }

    // сопоставления классов C# с коллекциями MongoDB
    public static void ShowBsonClassMap() {
        BsonClassMap.RegisterClassMap<Person>(cm => {
            cm.AutoMap();
            cm.MapMember(p => p.Name).SetElementName("username");
        });
        Person tom = new Person { Name = "Tom", Age = 90 };
        Console.WriteLine(tom.ToBsonDocument());
    }
}