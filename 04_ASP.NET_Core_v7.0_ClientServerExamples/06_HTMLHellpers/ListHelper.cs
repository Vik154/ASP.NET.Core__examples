using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace _06_HTMLHellpers;

public static class ListHelper {

    public static HtmlString CreateList(this IHtmlHelper html, string[] items) {

        // В конструктор TagBuilder передается элемент, для которого создается тег.
        TagBuilder ul = new TagBuilder("ul");

        foreach (string item in items) {
            TagBuilder li = new TagBuilder("li");
            li.InnerHtml.Append(item);              // Добавление текста в li
            ul.InnerHtml.AppendHtml(li);            // Добавление li в ul
        }
        ul.Attributes.Add("class", "itemsList");
        using var writer = new StringWriter();
        ul.WriteTo(writer, HtmlEncoder.Default);
        return new HtmlString(writer.ToString());
    }
}
