using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NostalgiaMudEngine.Core.Extensions;
using NostalgiaMudEngine.Core.Interfaces;
using NostalgiaMudEngine.Core.Models.EventArgs;
using NostalgiaMudEngine.Core.Models.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Systems
{
    public class ConnectivitySystem : ISystem
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<ConnectivitySystem> _logger;
        private readonly IOptionsMonitor<ConnectivitySystemOptions> _options;

        private readonly ConcurrentQueue<string> _userIdsToConnect = new ConcurrentQueue<string>();
        private readonly ConcurrentQueue<string> _userIdsToDisconnect = new ConcurrentQueue<string>();

        public ConnectivitySystem(
            IEventDispatcher eventDispatcher,
            ILogger<ConnectivitySystem> logger,
            IOptionsMonitor<ConnectivitySystemOptions> options
            )
        {
            _eventDispatcher = eventDispatcher.ThrowIfArgumentNull<IEventDispatcher>(nameof(eventDispatcher));
            _logger = logger.ThrowIfArgumentNull<ILogger<ConnectivitySystem>>(nameof(logger));
            _options = options.ThrowIfArgumentNull<IOptionsMonitor<ConnectivitySystemOptions>>(nameof(options));

            _eventDispatcher.GameTick += OnGameTick;
            _eventDispatcher.UserConnected += OnUserConnected;
            _eventDispatcher.UserDisconnected += OnUserDisconnected;
        }

        private void OnGameTick(object sender, GameTickEventArgs eventArgs)
        {
            // Handle new connections.
            // If no living character

            // Handle new disconnections.
        }

        private void OnUserConnected(object sender, UserConnectedEventArgs eventArgs)
        {
            _userIdsToConnect.Enqueue(eventArgs.UserId);
        }

        private void OnUserDisconnected(object sender, UserDisconnectedEventArgs eventArgs)
        {
            _userIdsToDisconnect.Enqueue(eventArgs.UserId);
        }
    }
}
