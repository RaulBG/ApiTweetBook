using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TweetBook.Installer
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "TweetBook API", Version = "v1", Description = "My new API Document" });
            });

            #region Aclaracion Swagger
            //builder.Services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Version = "v1",
            //        Title = "ToDo API",
            //        Description = "An ASP.NET Core Web API for managing ToDo items",
            //        TermsOfService = new Uri("https://example.com/terms"),
            //        Contact = new OpenApiContact
            //        {
            //            Name = "Example Contact",
            //            Url = new Uri("https://example.com/contact")
            //        },
            //        License = new OpenApiLicense
            //        {
            //            Name = "Example License",
            //            Url = new Uri("https://example.com/license")
            //        }
            //    });
            //});
            #endregion

        }
    }
}
