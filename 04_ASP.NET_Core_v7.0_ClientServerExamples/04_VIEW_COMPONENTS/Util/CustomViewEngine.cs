using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
namespace _04_VIEW_COMPONENTS.Util;

// Движок представлений реализует интерфейс IViewEngine
public class CustomViewEngine : IViewEngine {

    // В методе FindView() формируем путь к представлению, которое у нас находится в папке Views
    // и имеет в качестве расширения файла "html"
    public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage) {
        
        string viewPath = $"Views/Engine/{viewName}.html"; ;
        
        if (string.IsNullOrEmpty(viewName)) {
            viewPath = $"Views/Engine/{context.RouteData.Values["action"]}.html";
        }
        if (File.Exists(viewPath)) {
            return ViewEngineResult.Found(viewPath, new CustomView(viewPath));
        }
        else {
            return ViewEngineResult.NotFound(viewName, new string[] { viewPath });
        }
    }

    //  ViewEngineResult, указывает, что представление не найдено.
    public ViewEngineResult GetView(string? executingFilePath, string viewPath, bool isMainPage) {
        return ViewEngineResult.NotFound(viewPath, new string[] { });
    }
}
