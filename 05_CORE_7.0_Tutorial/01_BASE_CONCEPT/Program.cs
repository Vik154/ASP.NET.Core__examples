using _01_BASE_CONCEPT.Services;
using System.Text;

namespace _01_BASE_CONCEPT;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        // var app = builder.Build();

        /*** 07 - �������� ����� � ����������� ��������� ***/
        // ���������� ����� SecretCodeConstraint �� inline-����������� secretcode
        builder.Services.Configure<RouteOptions>(options =>
                        options.ConstraintMap.Add("secretcode", typeof(SecretCodeConstraint)));
        
        // �������������� ���������� ������ �����������
        // builder.Services.AddRouting(options => options.ConstraintMap.Add("secretcode", typeof(SecretConstraint)));
        var app = builder.Build();
        app.Map("/users/{name}/{token:secretcode(123466)}/",
            (string name, int token) => $"Name: {name} \nToken: {token}");

        app.Map("/", () => "Index Page");


        // MyMaps.SetMaps(ref app);
        // MyMaps.ShowMaps();

        /*
        app.Map("/", () => "Index page");
        app.Map("/about", () => "About page");
        app.Map("/html", async (context) => {
            context.Response.ContentType = "text/html";
            await context.Response.SendFileAsync("html/htmlpage.html"); });
        app.Map("/test", async (context) => await context.Response.WriteAsync("test"));

        app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpoints) => {
            // string.Join("\n", endpoints.SelectMany(e => e.Endpoints));
            var sb = new StringBuilder();
            var endpoints_ = endpoints.SelectMany(es => es.Endpoints);
            foreach (var endpoint in endpoints_) {
                sb.AppendLine(endpoint.DisplayName);

                // ������� �������� ����� ��� RouteEndpoint
                if (endpoint is RouteEndpoint routeEndpoint) {
                    sb.AppendLine(routeEndpoint.RoutePattern.RawText);
                }

                // ��������� ����������
                // ������ �������������
                // var routeNameMetadata = endpoint.Metadata.OfType<Microsoft.AspNetCore.Routing.RouteNameMetadata>().FirstOrDefault();
                // var routeName = routeNameMetadata?.RouteName;
                // ������ http - �������������� ���� ��������
                //var httpMethodsMetadata = endpoint.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault();
                //var httpMethods = httpMethodsMetadata?.HttpMethods; // [GET, POST, ...]
            }
            return sb.ToString();
        });
        */

        // 06 - ������������� ����������� ��������, � ����� ������������
        // var valueStorage = new ValueStorage();
        // builder.Services.AddSingleton<IGenerator>(valueStorage);
        // builder.Services.AddSingleton<IReader>(valueStorage);
        // var app = builder.Build();
        // app.UseMiddleware<GeneratorMiddleware>();
        // app.UseMiddleware<ReaderMiddleware>();

        // 05 - ��������� ��������� ������ ��������.
        // AddSingleton ������� ���� ������ ��� ���� ����������� ��������,
        // ��� ���� ������ ��������� ������ �����, ����� �� ��������������� ���������
        // builder.Services.AddSingleton<ICounter, RandomCounter>();
        // builder.Services.AddSingleton<CounterService>();

        // AddScoped ������� ���� ��������� ������� ��� ����� �������
        // builder.Services.AddScoped<ICounter, RandomCounter>();
        // builder.Services.AddScoped<CounterService>();

        // AddTransient() ������� transient-�������. ����� ������� ��������� ��� ������ ��������� � ���
        // builder.Services.AddTransient<ICounter, RandomCounter>();
        // builder.Services.AddTransient<CounterService>();
        // var app = builder.Build();
        // app.UseMiddleware<CounterMiddleware>();

        // 04 - Middleware � �������
        // ��� ���������� ���������� middleware, ������� ������������ �����,
        // � �������� ��������� ������� ����������� ����� UseMiddleware().
        // app.UseMiddleware<_04_MiddlewareClass>();
        // app.Run(async context => await context.Response.WriteAsync("Hello"));

        // 03 - Map �������� ����� �������� ������� ����� ������������ ������� �� ���������� ����
        // app.Map("/time", _03_MiddlewareMap.Step1);
        // app.Map("/About", ap => ap.Run(async ct => await ct.Response.WriteAsync("About")));
        // app.Map("/Index", ap => ap.Run(async ct => await ct.Response.WriteAsync("Index")));
        // app.Run(async ct => await ct.Response.WriteAsync("Hello Timer"));

        // 02 - UseWhen / MapWhen �������� ����� �������� middleware
        // app.MapWhen(_02_MiddlewareUseWhen.Step1, _02_MiddlewareUseWhen.Step2);
        // app.UseWhen(_02_MiddlewareUseWhen.Step1, _02_MiddlewareUseWhen.Step2);
        // app.Run(_02_MiddlewareUseWhen.Step3);

        // 01 - Use - �������� ��������� ������� ��������� ���������� �������� middleware
        // app.Use(_01_MiddlewareUse.Step3);
        // app.Run(_01_MiddlewareUse.Step4);
        // app.Use(_01_MiddlewareUse.Step1);
        // app.Run(_01_MiddlewareUse.Step2);

        // 01 - �������� ����
        // app.Run(SenderForm.SendHtmlForm);
        // <----> ���� � ����
        /*app.Run(async context => {
            context.Response.ContentType = "text/html";
            await context.Response.SendFileAsync("html/htmlpage.html");
        });
        */
        app.Run();
    }
}

/*
 * 1 - USE:
 
var builder = WebApplication.CreateBuilder();
var app = builder.Build();
 
string date = "";
 
app.Use(async(context, next) => {
    date = DateTime.Now.ToShortDateString();     // �������� �� �������� ������� ������ �� �������� 
    await next.Invoke();                         // �������� middleware �� app.Run
    Console.WriteLine($"Current date: {date}");  // �������� ����� ��������� ������� ��������� middleware
});
 
// ��������� middleware
app.Run(async(context) => await context.Response.WriteAsync($"Date: {date}"));
 
app.Run();
 
 */