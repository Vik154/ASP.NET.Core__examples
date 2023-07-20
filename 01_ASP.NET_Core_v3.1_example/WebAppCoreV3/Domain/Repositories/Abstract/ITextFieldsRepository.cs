using System;
using System.Linq;
using WebAppCoreV3.Domain.Entities;

namespace WebAppCoreV3.Domain.Repositories.Abstract {

    public interface ITextFieldsRepository {
        
        IQueryable<TextField> GetTextFields();
        TextField GetTextFieldById(Guid id);
        TextField GetTextFieldByCodeWord(string codeWord);
        void SaveTextField(TextField entity);
        void DeleteTextField(Guid id);
    }
}
