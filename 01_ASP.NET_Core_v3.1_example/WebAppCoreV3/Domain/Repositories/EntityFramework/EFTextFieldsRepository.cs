// Реализация интерфейса ITextFieldsRepository для работы с текстовыми полями
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using WebAppCoreV3.Domain.Entities;
using WebAppCoreV3.Domain.Repositories.Abstract;

namespace WebAppCoreV3.Domain.Repositories.EntityFramework {

    public class EFTextFieldsRepository : ITextFieldsRepository {
        private readonly AppDbContext context;
        public EFTextFieldsRepository(AppDbContext context) {
            this.context = context;
        }

        public IQueryable<TextField> GetTextFields() {
            return context.TextFields;
        }

        public TextField GetTextFieldById(Guid id) {
            return context.TextFields.FirstOrDefault(x => x.Id == id);
        }

        public TextField GetTextFieldByCodeWord(string codeWord) {
            return context.TextFields.FirstOrDefault(x => x.CodeWord == codeWord);
        }

        public void SaveTextField(TextField entity) {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteTextField(Guid id) {
            context.TextFields.Remove(new TextField() { Id = id });
            context.SaveChanges();
        }
    }

}