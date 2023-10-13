using UnityEngine.EventSystems;

namespace CoinGame.GameSystems.Actors
{
    public interface ICoin : IEventSystemHandler
    {
        void Obtain();
    }
}