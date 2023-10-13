using System;
using System.Collections.Generic;
using System.Linq;
using CoinGame.GameSystems.Actors;
using CoinGame.GameSystems.Presentation;
using CoinGame.GameSystems.Signals;
using Unity.Logging;

namespace CoinGame.GameSystems
{
    public class CoinCounter : IDisposable
    {
        public event Action CoinCollected;
        
        private int totalCount;
        
        private readonly ICoinCountPresentation _presentation;
        private readonly SignalBus<CoinGetSignal> _coinGetSignalBus;
        private readonly List<ICoin> _coins;

        private int CurrentGetCoinCount => totalCount - _coins.Count;

        private bool IsCollect => !_coins.Any();
        
        public CoinCounter(List<ICoin> allCoin, SignalBus<CoinGetSignal> signalBus, ICoinCountPresentation presentation)
        {
            _coins = new List<ICoin>(allCoin);
            _coinGetSignalBus = signalBus;
            _presentation = presentation;
        }

        private void OnGetCoin(CoinGetSignal signal)
        {
            Log.Info("Get Coin !");
            _coins.Remove(signal.GetCoin);
            if (IsCollect)
            {
                CoinCollected?.Invoke();
            }
            _presentation.ShowCoinCount(CurrentGetCoinCount, totalCount);
        }
        
        public void CountStart()
        {
            _coinGetSignalBus.Subscribe(OnGetCoin);
            totalCount = _coins.Count;
            _presentation.ShowCoinCount(CurrentGetCoinCount, totalCount);
        }

        public void Dispose()
        {
            _coinGetSignalBus.Unsubscribe(OnGetCoin);
        }
    }
}