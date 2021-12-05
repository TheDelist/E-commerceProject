using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.business.Abstract;
using E_commerceProject.business.Concrete;
using E_commerceProject.data.Abstract;
using E_commerceProject.data.Concrete.MSSQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace E_commerceProject.webui
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
             services.AddScoped<IProductRepository,SQLProductRepository>();
           

           
            services.AddScoped<IProductService,ProductManager>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           // app.UseHttpsRedirection();
            // app.UseAuthorization();
             app.UseRouting();
            app.UseStaticFiles();

           

          

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "about",
                    pattern: "about",
                    defaults:new {controller="Product",action="about"}
                );
                endpoints.MapControllerRoute(
                    name: "products2",
                    pattern: "products",
                    defaults:new {controller="Product",action="List"}
                );
                
                endpoints.MapControllerRoute(
                    name: "products",
                    pattern: "products/{category?}",
                    defaults:new {controller="Product",action="List"}
                );
              

                endpoints.MapControllerRoute(
                    name: "productdetails",
                    pattern: "{url}",
                    defaults:new {controller="Product",action="details"}
                );
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
