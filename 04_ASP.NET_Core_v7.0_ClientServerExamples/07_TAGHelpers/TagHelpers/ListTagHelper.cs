using Microsoft.AspNetCore.Razor.TagHelpers;
namespace _07_TAGHelpers.TagHelpers;

public class ListTagHelper : TagHelper {

    public List<string> Elements { get; set; } = new();
    public override void Process(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "ul";
        string listContent = "";
        foreach (string element in Elements) {
            listContent = $"{listContent}<li>{element}</li>";
        }
        output.Content.SetHtmlContent(listContent);
    }
}
