using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Dentistcalendar.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dentistcalendar.Data.Entity;

namespace Dentistcalendar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, AppRole>(options =>  {
                options.User.RequireUniqueEmail = true;   //email zorunlu mu
                // options.User.AllowedUserNameCharacters = ""; //izin verilen karakterler
                options.SignIn.RequireConfirmedEmail = false; // email dogrulama 
                options.SignIn.RequireConfirmedPhoneNumber = false; // tel isteme
                options.Password.RequireDigit = false; // rakam zorunlumu þifrede
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;// kucuk karakter olmalý 
                options.Password.RequireLowercase = false;// buyuk karakter olmalý 
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";//giris yapmamýssa logine yonlendir
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/Denied"; //erisim engel varsa 
                options.Cookie.Name = "Dentist.Cookie";
                options.SlidingExpiration = true; 
            });



            services.AddControllersWithViews();
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();// bu eklenti ile projem runtime anýnda derlem e yapacak hale geldý
        }

        private void AddDefaultTokenProviders()
        {
            throw new NotImplementedException();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Profile}/{action=Index}/{id?}");//ilk run ettýgýmýzde acýlacak aksiyon
                endpoints.MapRazorPages();
            });
        }
    }
}
