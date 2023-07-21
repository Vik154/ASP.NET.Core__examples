// Класс-помощник, точка входа для DataBase контекста
using WebAppCoreV3.Domain.Repositories.Abstract;

namespace WebAppCoreV3.Domain {

    public class DataManager {
    
        public ITextFieldsRepository TextFields { get; set; }
        public IServiceItemsRepository ServiceItems { get; set; }

        public DataManager(ITextFieldsRepository textFields, IServiceItemsRepository serviceItems) {
            TextFields = textFields;
            ServiceItems = serviceItems;
        }
    }
}