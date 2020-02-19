using Microsoft.Extensions.Logging;
using NostalgiaMudEngine.Core.Extensions;
using NostalgiaMudEngine.Core.Interfaces;
using NostalgiaMudEngine.Core.Models.EventArgs;
using NostalgiaMudEngine.Core.Models.InputOutput;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NostalgiaMudEngine.Core.Systems
{
    public class InputSystem
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<InputSystem> _logger;

        private readonly ConcurrentQueue<Input> _inputs = new ConcurrentQueue<Input>();
        
        public InputSystem(
            IEventDispatcher eventDispatcher,
            ILogger<InputSystem> logger
            )
        {
            _eventDispatcher = eventDispatcher.ThrowIfArgumentNull<IEventDispatcher>(nameof(eventDispatcher));
            _logger = logger.ThrowIfArgumentNull<ILogger<InputSystem>>(nameof(logger));

            _eventDispatcher.GameTick += OnGameTick;
            _eventDispatcher.InputSubmitted += OnInputSubmitted;
        }
        
        private void OnGameTick(object sender, GameTickEventArgs eventArgs)
        {
            // process user inputs
            // process npc inputs
        }

        private void OnInputSubmitted(object sender, InputSubmittedEventArgs eventArgs)
        {
            _inputs.Enqueue(new Input()
            {
                UserId = eventArgs.UserId,
                Value = eventArgs.InputValue
            });
        }
    }
}
