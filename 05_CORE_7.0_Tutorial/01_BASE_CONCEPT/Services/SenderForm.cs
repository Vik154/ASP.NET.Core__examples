namespace _01_BASE_CONCEPT.Services;

public class SenderForm {

    /// <summary> Отправка Html форм </summary>
    public static async Task SendHtmlForm(HttpContext context) {
        context.Response.ContentType = "text/html; charset=utf-8";

        if (context.Request.Path == "/postuser") {
            var form = context.Request.Form;
            string? name = form["name"];
            string? age = form["age"];
            await context.Response.WriteAsync($"<div><p>Name: {name}</p><p>Age: {age}</p></div>");
        }
        else {
            await context.Response.SendFileAsync("html/htmlpage.html");
        }
    }
}
