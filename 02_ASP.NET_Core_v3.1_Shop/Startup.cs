using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.DataBase;
using Shop.Interfaces;
using Shop.Mocks;
using Shop.Models;
using Shop.Repository;

namespace Shop {
    public class Startup {

        private IConfigurationRoot _confString;
        
        
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting) {
            _confString = new ConfigurationBuilder()
                .SetBasePath(hosting.ContentRootPath)
                .AddJsonFile("dbsettings.json")
                .Build();
        }
        

        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<AppDBContent>((options) => {
                options.UseSqlServer(_confString.GetConnectionString("DefaultConnection"));
            });
            // Связывание между собой класса и интерфейса
            services.AddTransient<IAllCars, CarRepository>();
            services.AddTransient<ICarsCategory, CategoryRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShopCart.GetCart(sp));

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            app.UseDeveloperExceptionPage();    // Отображение страницы ошибок
            app.UseStatusCodePages();           // Отображение кодов ошибок
            app.UseStaticFiles();               // Отображение css картинок и пр.
            app.UseSession();
           // app.UseMvcWithDefaultRoute();       // Отслеживание url-адреса

            using (var scope = app.ApplicationServices.CreateScope()) {
                AppDBContent content = 
                    scope.
                    ServiceProvider.
                    GetRequiredService<AppDBContent>();
                
                DBObjects.Initial(content);
            }
            
        }
    }
}
