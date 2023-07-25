using System.Text.Json.Serialization;
using System.Text.Json;

namespace BaseServer;

// public record Person(string Name, int Age); --- Лежит 01_Base...
class PersonConverter : JsonConverter<Person> {

    // При наследовании класса JsonConverter необходимо реализовать его абстрактные методы:
    // Read() (выполняет десериализацию из JSON в Person)
    public override Person Read(
        ref Utf8JsonReader reader,          /* Utf8JsonReader - объект, который читает данные из json */
        Type typeToConvert,                 /* Type - тип, в который надо выполнить конвертацию */
        JsonSerializerOptions options)      /* JsonSerializerOptions - дополнительные параметры сериализации */
    {    
        var personName = "Undefined";       // В начале определяем данные объекта Person по умолчанию, которые
        var personAge = 0;                  // будут применяться, если в процессе десериализации произойдут проблемы

        // Далее в цикле считываем каждый токен в строке json с помощью метода Read() объекта Utf8JsonReader:
        while (reader.Read()) {

            // Затем, если считанный токен представляет название свойства,
            // то считываем его и считываем следующий токен:
            if (reader.TokenType == JsonTokenType.PropertyName) {
                var propertyName = reader.GetString();
                reader.Read();

                // После этого мы можем узнать, как называется свойство и какое значение оно имеет.
                // Для этого применяем конструкцию switch:
                switch (propertyName?.ToLower()) {

                    // если свойство age и оно содержит число
                    case "age" when reader.TokenType == JsonTokenType.Number:
                        personAge = reader.GetInt32();  // считываем число из json
                        break;

                    // если свойство age и оно содержит строку
                    case "age" when reader.TokenType == JsonTokenType.String:
                        string? stringValue = reader.GetString();
                        // пытаемся конвертировать строку в число
                        if (int.TryParse(stringValue, out int value)) {
                            personAge = value;
                        }
                        break;

                    // если свойство Name/name
                    case "name":    
                        string? name = reader.GetString();
                        if (name != null)
                            personName = name;
                        break;
                }
            }
        }
        // В конце полученными данными инициализируем объект Person и возвращаем его из метода:
        return new Person(personName, personAge);
    }

    // Write() (выполняет сериализацию из Person в JSON).
    public override void Write(
        Utf8JsonWriter writer,           /* Utf8JsonWriter - объект, который записывает данные в json */
        Person person,                   /* Person - объект, который надо сериализовать */
        JsonSerializerOptions options)   /* JsonSerializerOptions - дополнительные параметры сериализации */
    {
        writer.WriteStartObject();                  // с помощью объекта Utf8JsonWriter открываем запись объекта в формате json
        writer.WriteString("name", person.Name);    // Последовательно записываем данные объекта Person:
        writer.WriteNumber("age", person.Age);      // Последовательно записываем данные объекта Person:
        writer.WriteEndObject();                    // И завершаем запись объекта:
    }
}