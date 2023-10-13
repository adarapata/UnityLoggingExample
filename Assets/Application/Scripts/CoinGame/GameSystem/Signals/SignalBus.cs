using System;
using System.Collections.Generic;

namespace CoinGame.GameSystems.Signals
{
    public class SignalBus<TMessage>
    {
        private readonly List<Action<TMessage>> _actions = new();
        
        public void Subscribe(Action<TMessage> action)
        {
            _actions.Add(action);
        }
        
        public void Unsubscribe(Action<TMessage> action)
        {
            _actions.Remove(action);
        }
        
        public void Fire(TMessage message)
        {
            foreach (var action in _actions)
            {
                action?.Invoke(message);
            }
        }
    }
}