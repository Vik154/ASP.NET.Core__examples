using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace _04_VIEW_COMPONENTS.Util;

public class CustomView : IView {
    public string Path { get; set;}

    // Через конструктор получает путь к файлу представления
    public CustomView(string view_path) => Path = view_path;

    // происходит считывание файла
    public async Task RenderAsync(ViewContext context) {
        string content = "";
        using (StreamReader viewRader = new StreamReader(Path)) {
            content = await viewRader.ReadToEndAsync();
        }
        // Writer пишет в выходной поток считанное из файла содержимое.
        await context.Writer.WriteAsync(content);
    }
}
