using Microsoft.AspNetCore.Razor.TagHelpers;
namespace _07_TAGHelpers.TagHelpers;

public class MyStyleTagHelper : TagHelper {

    public bool SecondsIncluded { get; set; }
    public string? Color { get; set; }
    
    public override void Process(TagHelperContext context, TagHelperOutput output) {
        var now = DateTime.Now;
        var time = String.Empty;

        if (SecondsIncluded)    
            time = now.ToString("HH:mm:ss");
        else
            time = now.ToString("HH:mm");

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        // устанавливаем цвет, если свойство Color не равно null
        if (Color != null) output.Attributes.SetAttribute("style", $"color:{Color};");

        output.Content.SetContent(time);
    }
}
