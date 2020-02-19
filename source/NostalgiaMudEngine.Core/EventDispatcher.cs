using NostalgiaMudEngine.Core.Interfaces;
using NostalgiaMudEngine.Core.Models.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core
{
    public class EventDispatcher : IEventDispatcher
    {
        public event EventHandler<CharacterChangesSubmittedEventArgs> CharacterChangesSubmitted;
        public void OnCharacterUpdateSubmitted(object sender, CharacterChangesSubmittedEventArgs eventArgs)
        {
            CharacterChangesSubmitted?.Invoke(this, eventArgs);
        }

        public event EventHandler<GameStartedEventArgs> GameStarted;
        public void OnGameStarted(object sender, GameStartedEventArgs eventArgs)
        {
            GameStarted?.Invoke(this, eventArgs);
        }

        public event EventHandler<GameTickEventArgs> GameTick;
        public void OnGameTick(object sender, GameTickEventArgs eventArgs)
        {
            GameTick?.Invoke(this, eventArgs);
        }

        public event EventHandler<ShutdownRequestedEventArgs> ShutdownRequested;
        public void OnShutdownRequested(object sender, ShutdownRequestedEventArgs eventArgs)
        {
            ShutdownRequested?.Invoke(this, eventArgs);
        }

        public event EventHandler<UserConnectedEventArgs> UserConnected;
        public void OnUserConnected(object sender, UserConnectedEventArgs eventArgs)
        {
            UserConnected?.Invoke(this, eventArgs);
        }

        public event EventHandler<UserDisconnectedEventArgs> UserDisconnected;
        public void OnUserDisconnected(object sender, UserDisconnectedEventArgs eventArgs)
        {
            UserDisconnected?.Invoke(this, eventArgs);
        }

        public event EventHandler<InputSubmittedEventArgs> InputSubmitted;
        public void OnInputSubmitted(object sender, InputSubmittedEventArgs eventArgs)
        {
            InputSubmitted?.Invoke(this, eventArgs);
        }
    }
}
