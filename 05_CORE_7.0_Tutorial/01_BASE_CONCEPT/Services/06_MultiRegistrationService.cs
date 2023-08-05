// Множественная регистрация сервисов
namespace _01_BASE_CONCEPT.Services;

// Генерация рандомного значения
public interface IGenerator {
    int GenerateValue();
} 

// Чтение сгенерированного значения
public interface IReader {
    int ReadValue();
}

// Для обеих зависимостей - IGenerator и IReader определена одна реализация - ValueStorage
public class ValueStorage : IGenerator, IReader {

    private int value;                      // Значение считанное / сгенерированное

    public int GenerateValue() {
        value = new Random().Next(0, 1000); // Генерация 
        return value;
    }
    public int ReadValue() => value;        // Чтение
}

// Конвейеры middleware
public class GeneratorMiddleware {

    RequestDelegate next;
    IGenerator generator;

    public GeneratorMiddleware(RequestDelegate next_, IGenerator generator_) {
        next = next_;
        generator = generator_;
    }

    public async Task InvokeAsync(HttpContext context) {
        if (context.Request.Path == "/generate")
            await context.Response.WriteAsync($"New value: {generator.GenerateValue()}");
        else
            await next.Invoke(context);
    }
}

public class ReaderMiddleware {
    IReader reader;
    public ReaderMiddleware(RequestDelegate _, IReader reader_) => reader = reader_;
    public async Task InvokeAsync(HttpContext context) {
        await context.Response.WriteAsync($"Current value: {reader.ReadValue()}");
    }
}