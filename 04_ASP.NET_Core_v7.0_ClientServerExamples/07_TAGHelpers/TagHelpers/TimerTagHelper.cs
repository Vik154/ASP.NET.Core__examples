using Microsoft.AspNetCore.Razor.TagHelpers;
namespace _07_TAGHelpers.TagHelpers;

public class TimerTagHelper : TagHelper {

    // Для генерации элемента html на основе тега используется метод Process
    public override void Process(
        TagHelperContext context,   // объект TagHelperContext, представляющий контекст тега (его содержимое, атрибуты)
        TagHelperOutput output)     // объект TagHelperOutput, отвечает за генерацию выходного элемента html на основе тега
    {
        output.TagName = "div";    // заменяет тег <timer> тегом <div>
        output.Content.SetContent( // устанавливаем содержимое элемента
            $"Текущее время: {DateTime.Now.ToString("HH:mm:ss")}");
    }
}
