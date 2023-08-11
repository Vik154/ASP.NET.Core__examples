using MongoDB.Bson;
using MongoDB.Driver;

namespace _01_MongoDB;

internal class Program {

    static async Task Main(string[] args) {

        // Пример сортировки
        await DataFilter.TestFilter.SortBson();

        // Пример поиска в БД по фильтрам
        // await DataFilter.TestFilter.ShowResult();
        //await DataFilter.TestFilter.FilterDef();

        // Пример получения документов из БД
        // await AddElement.TestElem.GetDoc();
        // await AddElement.TestElem.GetCSClass();

        // Пример добавления элементов
        // await AddElement.TestElem.ShowResult();

        // Настройка модели с помощью атрибутов
        // AttrModels.TestAttr.ShowResult();
        // AttrModels.TestAttr.ShowBsonClassMap();

        // Конвертация в BSON и обратно
        // TestModel.ShowResult();
        // TestModel.ShowSwapTypes();

        // Базовые концепции MongoDB
        // await _01_BASE.ShowResult();
    }
}