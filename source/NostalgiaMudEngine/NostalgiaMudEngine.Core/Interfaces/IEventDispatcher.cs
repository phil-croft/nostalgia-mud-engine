using NostalgiaMudEngine.Core.Models.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Interfaces
{
    public interface IEventDispatcher
    {
        event EventHandler<GameTickEventArgs> GameTick;
        void OnGameTick(object sender, GameTickEventArgs eventArgs);
        
        event EventHandler<GameStartedEventArgs> GameStarted;
        void OnGameStarted(object sender, GameStartedEventArgs eventArgs);

        event EventHandler<ShutdownRequestedEventArgs> ShutdownRequested;
        void OnShutdownRequested(object sender, ShutdownRequestedEventArgs eventArgs);

        event EventHandler<CharacterChangesSubmittedEventArgs> CharacterChangesSubmitted;
        void OnCharacterUpdateSubmitted(object sender, CharacterChangesSubmittedEventArgs eventArgs);

        event EventHandler<UserConnectedEventArgs> UserConnected;
        void OnUserConnected(object sender, UserConnectedEventArgs eventArgs);

        event EventHandler<UserDisconnectedEventArgs> UserDisconnected;
        void OnUserDisconnected(object sender, UserDisconnectedEventArgs eventArgs);

        event EventHandler<InputSubmittedEventArgs> InputSubmitted;
        void OnInputSubmitted(object sender, InputSubmittedEventArgs eventArgs);
    }
}
