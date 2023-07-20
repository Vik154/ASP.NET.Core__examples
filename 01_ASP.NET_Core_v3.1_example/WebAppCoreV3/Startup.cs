using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreV3.Service;

namespace WebAppCoreV3 {

    public class Startup {

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services) {
            // Подключаем конфиг из appsetting.json
            Configuration.Bind("Project", new Config());

            // Добавляем поддержку контроллеров и представлений (MVC)
            services.AddControllersWithViews()
                // Выставляем совместимость с версией ap.net core 3.0
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            
            // Порядок регистрации middleware очень важен
            // Подробная информация об ошибках в процессе разработки
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            
            app.UseRouting();

            // Подключение поддержки статичных файлов в приложении (css, js)
            app.UseStaticFiles();

            // Регистрация нужных маршрутов
            app.UseEndpoints( (endpoints) => {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
