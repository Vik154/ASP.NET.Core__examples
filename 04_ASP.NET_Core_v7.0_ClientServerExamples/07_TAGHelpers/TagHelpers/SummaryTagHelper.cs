using Microsoft.AspNetCore.Razor.TagHelpers;
namespace _07_TAGHelpers.TagHelpers;

public class SummaryTagHelper : TagHelper {

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "div";
        var target = await output.GetChildContentAsync();
        var content = "<h3>Общая информация:</h3>" + target.GetContent();
        output.Content.SetHtmlContent(content);
    }
}
