using Microsoft.AspNetCore.Razor.TagHelpers;
namespace _07_TAGHelpers.TagHelpers;

public class AdvancedTagHelper : TagHelper {
    public override void Process(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.PreElement.SetHtmlContent("<h4>Дата и время</h4>");
        output.PostElement.SetHtmlContent($"<div>Дата: {DateTime.Now.ToString("dd:MM:yyyy")}</div>");
        output.Content.SetContent($"Время: {DateTime.Now.ToString("HH:MM:ss")}");
    }
}
