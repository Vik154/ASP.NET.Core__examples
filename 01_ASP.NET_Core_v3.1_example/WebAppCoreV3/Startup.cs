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
            // ���������� ������ �� appsetting.json
            Configuration.Bind("Project", new Config());

            // ���������� ������ ���������� ���������� � �������� ��������
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            // ���������� �������� ��
            services.AddDbContext<AppDbContext>((context) => context.UseSqlServer(Config.ConnectionString));

            // ����������� identity �������
            services.AddIdentity<IdentityUser, IdentityRole>((opts) => {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            // ����������� authentication cooke
            services.ConfigureApplicationCookie((options) => {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            // ��������� ��������� ������������ � ������������� (MVC)
            services.AddControllersWithViews()
                // ���������� ������������� � ������� ap.net core 3.0
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            
            // ������� ����������� middleware ����� �����
            // ��������� ���������� �� ������� � �������� ����������
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            
            // ����������� ��������� ��������� ������ � ���������� (css, js)
            app.UseStaticFiles();

            // ����������� ������� �������������
            app.UseRouting();

            // ����������� �������������� � �����������
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            // ����������� ������ ���������
            app.UseEndpoints( (endpoints) => {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
