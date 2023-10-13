using CoinGame.GameSystems.Actors;
using CoinGame.GameSystems.Signals;
using UnityEngine;

namespace CoinGame.Coin
{
    public class CoinBehaviour : MonoBehaviour, ICoin
    {
        private SignalBus<CoinGetSignal> _signalBus;

        public void Construct(SignalBus<CoinGetSignal> signalBus)
        {
            _signalBus = signalBus;
        }

        public void Obtain()
        {
            var signal = new CoinGetSignal(this);
            // コインが取得されたことを通知する
            _signalBus.Fire(signal);
            Destroy(gameObject);
        }
    }
}
