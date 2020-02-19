using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NostalgiaMudEngine.Core.Extensions;
using NostalgiaMudEngine.Core.Interfaces;
using NostalgiaMudEngine.Infrastructure.Data;
using NostalgiaMudEngine.Web.Services;

namespace NostalgiaMudEngine.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
                .ConfigureAppConfiguration((builderContext, builder) =>
                {
                    var env = builderContext.HostingEnvironment;

                    builder.SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json",
                            optional: false,
                            reloadOnChange: false)
                       .AddEnvironmentVariables();

                    if (env.IsDevelopment())
                    {
                        builder.AddUserSecrets<Startup>();
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging();

                    services.AddDbContext<EFDataContext>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("Default")));

                    services.AddCoreDependencies(hostContext.Configuration);
                    services.AddSingleton<IClientMessageService, ClientMessageService>();

                    services.AddHostedService<GameBackgroundService>();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseWebRoot("Identity\\wwwroot")
                        .UseStartup<Startup>();
                });
    }
}
