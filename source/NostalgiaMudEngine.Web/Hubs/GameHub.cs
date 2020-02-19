using Microsoft.AspNetCore.SignalR;
using NostalgiaMudEngine.Core.Extensions;
using NostalgiaMudEngine.Core.Interfaces;
using NostalgiaMudEngine.Core.Models.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NostalgiaMudEngine.Web.Hubs
{
    public class GameHub : Hub
    {
        private readonly IEventDispatcher _eventDispatcher;

        public GameHub(
            IEventDispatcher eventDispatcher
            )
        {
            _eventDispatcher = eventDispatcher.ThrowIfArgumentNull<IEventDispatcher>(nameof(eventDispatcher));
        }

        public override async Task OnConnectedAsync()
        {
            _eventDispatcher.OnUserConnected(this, new UserConnectedEventArgs()
            {
                 UserId = Context.UserIdentifier
            });

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _eventDispatcher.OnUserConnected(this, new UserConnectedEventArgs()
            {
                UserId = Context.UserIdentifier
            });

            await base.OnDisconnectedAsync(exception);
        }

        public void OnUserInputSubmitted(string inputValue)
        {
            _eventDispatcher.OnInputSubmitted(this, new InputSubmittedEventArgs()
            {
                UserId = Context.UserIdentifier,
                InputValue = inputValue
            });
        }
    }
}
