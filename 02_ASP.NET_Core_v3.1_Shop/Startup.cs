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
            // ���������� ����� ����� ������ � ����������
            services.AddTransient<IAllCars, CarRepository>();
            services.AddTransient<ICarsCategory, CategoryRepository>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();               // ����������� css �������� � ��.
            app.UseRouting();

            app.UseCookiePolicy();

            app.UseDeveloperExceptionPage();    // ����������� �������� ������
            app.UseStatusCodePages();           // ����������� ����� ������
            // app.UseMvcWithDefaultRoute();       // ������������ url-������

            using (var scope = app.ApplicationServices.CreateScope()) {
                AppDBContent content = 
                    scope.
                    ServiceProvider.
                    GetRequiredService<AppDBContent>();
                
                DBObjects.Initial(content);
            }
            
            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", async context => {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
            
        }
    }
}
