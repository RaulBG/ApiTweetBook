using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace TweetBook.Installer
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installersCollection = typeof(Startup).Assembly.ExportedTypes.Where(x => typeof(IInstaller).IsAssignableFrom(x)
             && !x.IsInterface
             && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>().ToList();
            //  installersCollection.ForEach(installer => installer.InstallServices(services, Configuration));
            // Creo que el forEach e mas eficiente seria cuestion de verlo
            foreach (var item in installersCollection)
            {
                item.InstallServices(services, configuration);
            }



        }
    }
}
