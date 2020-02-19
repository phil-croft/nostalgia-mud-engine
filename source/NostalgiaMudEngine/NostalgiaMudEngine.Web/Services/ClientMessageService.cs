using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using NostalgiaMudEngine.Core.Extensions;
using NostalgiaMudEngine.Core.Interfaces;
using NostalgiaMudEngine.Core.Models.InputOutput;
using NostalgiaMudEngine.Web.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NostalgiaMudEngine.Web.Services
{
    public class ClientMessageService : IClientMessageService
    {
        private readonly IHubContext<GameHub> _gameHub;
        private readonly ILogger<ClientMessageService> _logger;

        public ClientMessageService(
            IHubContext<GameHub> gameHub,
            ILogger<ClientMessageService> logger
            )
        {
            _gameHub = gameHub.ThrowIfArgumentNull<IHubContext<GameHub>>(nameof(gameHub));
            _logger = logger.ThrowIfArgumentNull<ILogger<ClientMessageService>>(nameof(logger));
        }

        public void Send(Output userOutput)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(userOutput.UserId))
                    throw new ArgumentNullException($"{nameof(userOutput.UserId)} cannot be empty.");

                _gameHub.Clients.User(userOutput.UserId).SendAsync("ReceiveOutput", userOutput);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unable to send client message.");
            }
        }
    }
}
