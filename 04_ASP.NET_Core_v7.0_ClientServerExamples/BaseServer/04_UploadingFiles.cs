// Загрузка файлов на сервер
namespace BaseServer;

public class UploadingFiles {

    public static void RunApplication(string[] args) {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        WebApplication app = builder.Build();

        app.Run(async (context) => {
            var response = context.Response;
            var request = context.Request;

            response.ContentType = "text/html; charset=utf-8";

            if (request.Path == "/upload" && request.Method == "POST") {
                IFormFileCollection files = request.Form.Files;
                // путь к папке, где будут храниться файлы
                var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
                // создаем папку для хранения файлов
                Directory.CreateDirectory(uploadPath);

                foreach (var file in files) {
                    // путь к папке uploads
                    string fullPath = $"{uploadPath}/{file.FileName}";

                    // сохраняем файл в папку uploads
                    using (var fileStream = new FileStream(fullPath, FileMode.Create)) {
                        await file.CopyToAsync(fileStream);
                    }
                }
                await response.WriteAsync("Файлы успешно загружены");
            }
            else {
                await response.SendFileAsync("html/UploadingFiles.html");
            }
        });

        app.Run();
    }
}