using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NostalgiaMudEngine.Core.Interfaces;
using NostalgiaMudEngine.Core.Models.Options;
using NostalgiaMudEngine.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEntityManager, EntityManager>();
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            
            // Options
            services.Configure<ConnectivitySystemOptions>(configuration);
            services.Configure<InputSystemOptions>(configuration);

            return services;
        }
    }
}
