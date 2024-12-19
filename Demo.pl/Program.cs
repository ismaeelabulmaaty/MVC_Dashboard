using Demo.BLL.Interfaces;
using Demo.BLL.Repo;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Demo.pl.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.pl
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services that Allow Dependenciy injection
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Scoped);

            //ApplicationServicesExtensions.AddApplicationServices(Services);
            ////Services//.//AddApplicationServices();

            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfils()));

            builder.Services.AddScoped<IunitOfWork, unitOfWork>();

            //Services.AddScoped<UserManager<ApplactionUser>>();
            //Services.AddScoped<SignInManager<ApplactionUser>>();
            //Services.AddScoped<RoleManager<IdentityRole>>();

            builder.Services.AddIdentity<ApplactionUser, IdentityRole>(Config =>
            {
                Config.Password.RequiredUniqueChars = 2;
                Config.Password.RequireUppercase = true;
                Config.Password.RequireLowercase = true;
                Config.Password.RequireDigit = true;
                Config.Password.RequireNonAlphanumeric = true;
                Config.User.RequireUniqueEmail = true;
                Config.Lockout.MaxFailedAccessAttempts = 3;
                Config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                Config.Lockout.AllowedForNewUsers = true;


            }).AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(Config =>
            {
                Config.LoginPath = "/Account/SignIn";
            });

            //Services.AddAuthentication("Cookies")
            //        .AddCookie(config =>
            //        {
            //            config.LoginPath = "/Home/SignIn";
            //            config.AccessDeniedPath = "/Home/Error";
            //        });

            #endregion

            var app = builder.Build();

            #region Configure Http Request piplibes

            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();// لازم تكون دي الاول
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
            });



            #endregion

            app.Run();

            //CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
