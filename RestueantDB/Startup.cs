using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestueantDB.Data;
using RestueantDB.Helper;
using RestueantDB.Mapper;
using RestueantDB.Models;
using RestueantDB.ModelViews;
using RestueantDB.Repostry.IServices;
using RestueantDB.Repostry.Services;
using RestueantDB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestueantDB
{
    public class Startup
    {

        //private MapperConfiguration _mapperConfiguration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //_mapperConfiguration = new MapperConfiguration(a =>
            //{
            //    a.AddProfile(new Mapping());
            //});
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddScoped<IResturant, ResturantRepo>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IMealServices, MealServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IEmailSevicess, EmailSevicess>();
            //services.AddIdentity<IdentityUser, IdentityRole>(options =>
            // {
            //options.SignIn.RequireConfirmedAccount = false;
            //options.SignIn.RequireConfirmedPhoneNumber = false;
            //options.SignIn.RequireConfirmedAccount = false;



            //}).AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddDefaultTokenProviders()
            //.AddDefaultUI()
            // .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>();


            services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 1;
                    options.Password.RequiredUniqueChars = 0;
                   

                }).AddRoles<IdentityRole>()
                   .AddRoleManager<RoleManager<IdentityRole>>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>().AddDefaultTokenProviders(); 

            services.ConfigureApplicationCookie(config => config.LoginPath = Configuration["Application:LoginPath"]);

            services.AddScoped<ApplicationUserClaimsPrincipalFactory>();

            services.Configure<SMTPConfig>(Configuration.GetSection("SMPTConfig"));

          
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Mapping));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapControllerRoute(

                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
            });
        }
    }
}
