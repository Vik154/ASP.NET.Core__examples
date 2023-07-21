using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreV3.Domain;
using WebAppCoreV3.Domain.Repositories.Abstract;
using WebAppCoreV3.Domain.Repositories.EntityFramework;
using WebAppCoreV3.Service;

namespace WebAppCoreV3 {

    public class Startup {

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services) {
            // Подключаем конфиг из appsetting.json
            Configuration.Bind("Project", new Config());

            // Подключаем нужный функционал приложения в качестве сервисов
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            // Подключаем контекст БД
            services.AddDbContext<AppDbContext>((context) => context.UseSqlServer(Config.ConnectionString));

            // Настраиваем identity систему
            services.AddIdentity<IdentityUser, IdentityRole>((opts) => {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            // Настраиваем authentication cooke
            services.ConfigureApplicationCookie((options) => {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            // Настройка политики авторизации для Admin area
            services.AddAuthorization((x) => {
                x.AddPolicy("AdminArea", (policy) => { policy.RequireRole("admin"); });
            });

            // Добавляем поддержку контроллеров и представлений (MVC)
            services.AddControllersWithViews((x) => {
                x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
            }) 
                // Выставляем совместимость с версией ap.net core 3.0
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            
            // Порядок регистрации middleware очень важен
            // Подробная информация об ошибках в процессе разработки
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            
            // Подключение поддержки статичных файлов в приложении (css, js)
            app.UseStaticFiles();

            // Подключение системы маршрутизации
            app.UseRouting();

            // Подключение аутентификации и авторизации
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            // Регистрация нужных маршрутов
            app.UseEndpoints( (endpoints) => {
                // Маршрут для администратора
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
