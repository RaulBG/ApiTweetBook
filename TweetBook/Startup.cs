using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TweetBook.Options;
using TweetBook.Installer;
using System.Reflection;
using System.Linq;
using System;
//using Swashbuckle.AspNetCore.Swagger;

namespace TweetBook
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
            services.InstallServicesInAssembly(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseDatabaseErrorPage(); https://youtu.be/sdlt3-ptt9g
            }
            else
            {
                // app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(option =>
                { option.RouteTemplate = swaggerOptions.JsonRoute; });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Version);
                options.RoutePrefix = string.Empty;
            });

            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("v1/swagger.json", "MyAPI V1");
            //});

            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            //    options.RoutePrefix = string.Empty;
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseCookiePolicy();           
            //app.UseAuthentication();

            app.UseMvc();
            //Se comenta para hacer la ruta del versionado
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
