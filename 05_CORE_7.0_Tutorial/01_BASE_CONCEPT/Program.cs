using _01_BASE_CONCEPT.Services;

namespace _01_BASE_CONCEPT;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.Run(SenderForm.SendHtmlForm);

        app.Run();
    }
}