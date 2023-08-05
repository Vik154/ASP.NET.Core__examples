// Жизненный цикл зависимостей и добавление сервисов используя свои классы middleware
namespace _01_BASE_CONCEPT.Services;

// Для рассмотрения механизма внедрения зависимостей и жизненного цикла возьмем следующий интерфейс ICounter:
public interface ICounter {
    int Value { get; }
}

// Реализация этого интерфейса - класс RandomCounter
public class RandomCounter : ICounter {
    private static Random random = new Random();
    private int _counter;

    public RandomCounter() => _counter = random.Next(0, 100_000);
    public int Value => _counter;
}

// Класс для демонстрации внедрения зависимостей (механизма Depedency Injection)
public class CounterService {
    public ICounter Counter { get; }
    public CounterService(ICounter counter) => Counter = counter;
}

// CounterMiddleware компонент middleware для работы с сервисами
public class CounterMiddleware {
    private RequestDelegate next;   // Переход к следующему middleware
    int i = 0;                      // Счетчик запросов

    public CounterMiddleware(RequestDelegate next_) => next = next_;

    // Для получения зависимостей используется метод InvokeAsync, в котором передаются две зависимости
    // ICounter и CounterService. В самом методе выводятся значения Value из обоих зависимостей.
    // Причем сервис CounterService сам использует зависимость ICounter.
    public async Task InvokeAsync(HttpContext context, ICounter counter, CounterService counterService) {
        i++;
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync(
            $"Запрос: {i}; Counter: {counter.Value}; Service: {counterService.Counter.Value}");
    }
}