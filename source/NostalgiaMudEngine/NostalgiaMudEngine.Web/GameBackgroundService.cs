using Microsoft.Extensions.Hosting;
using NostalgiaMudEngine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NostalgiaMudEngine.Web
{
    public class GameBackgroundService : BackgroundService
    {
        private IEventDispatcher _eventDispatcher;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => {
            
            });
        }
    }
}
