using _10_Entity_Framework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
namespace _10_Entity_Framework.TagHelpers;

public class SortHeaderTagHelper : TagHelper {

    public SortState Property { get; set; }     // значение текущего свойства, для которого создается тег
    public SortState Current  { get; set; }     // значение активного свойства, выбранного для сортировки
    public string?   Action   { get; set; }     // действие контроллера, на которое создается ссылка
    public bool      Up       { get; set; }     // сортировка по возрастанию или убыванию

    // Через механизм внедрения зависимостей через атрибут получаем контекст представления ViewContext,
    // в котором будет вызываться хелпер:
    // С помощью этого объекта мы сможем получить объект IUrlHelper, который необходим для создания ссылки.
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null;

    // Для создания адреса ссылки по методу контроллера потребуется объект IUrlHelperFactory
    IUrlHelperFactory urlHelperFactory { get; set; }    

    public SortHeaderTagHelper(IUrlHelperFactory helperFactory) {
        urlHelperFactory = helperFactory;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output) {
        IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
        output.TagName = "a";
        
        string? url = urlHelper.Action(Action, new { sortOrder = Property });
        output.Attributes.SetAttribute("href", url);
        
        // если текущее свойство имеет значение CurrentSort
        if (Current == Property) {
            TagBuilder tag = new TagBuilder("i");
            tag.AddCssClass("glyphicon");

            if (Up == true)   // если сортировка по возрастанию
                tag.AddCssClass("glyphicon-chevron-up");
            else   // если сортировка по убыванию
                tag.AddCssClass("glyphicon-chevron-down");

            output.PreContent.AppendHtml(tag);
        }
    }
}
