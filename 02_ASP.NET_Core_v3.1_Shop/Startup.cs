using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.DataBase;
using Shop.Interfaces;
using Shop.Mocks;
using Shop.Repository;

namespace Shop {
    public class Startup {

        private IConfigurationRoot _confString;
        
        public Startup(IHostingEnvironment hosting) {
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

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            app.UseDeveloperExceptionPage();    // Отображение страницы ошибок
            app.UseStatusCodePages();           // Отображение кодов ошибок
            app.UseStaticFiles();               // Отображение css картинок и пр.
            // app.UseMvcWithDefaultRoute();       // Отслеживание url-адреса

            /*
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", async context => {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
            */
        }
    }
}
