using UnityEngine;

namespace CoinGame.GameSystems.Actors
{
    public interface IPlayableActor
    {
        void Initialize(Vector3 position);
        void UpdateActor();
    }
}