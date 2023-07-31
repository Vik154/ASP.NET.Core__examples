// провайдер привязчика модели
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace _05_MODELS.Infrastructure;

public class CustomDateTimeModelBinderProvider : IModelBinderProvider {

    // GetBinder() - с помощью контекста провайдера ModelBinderProviderContext
    // мы можем получить тип данных, для которых выполняется привязка
    public IModelBinder? GetBinder(ModelBinderProviderContext context) {

        // Для объекта SimpleTypeModelBinder необходим сервис ILoggerFactory Получаем его из сервисов
        ILoggerFactory loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
        IModelBinder binder = new CustomDateTimeModelBinder(new SimpleTypeModelBinder(typeof(DateTime), loggerFactory));
        return context.Metadata.ModelType == typeof(DateTime) ? binder : null;
    }
}