using UnityEngine;

namespace CoinGame.PlayerCharacters
{
    public class MoveCalculator
    {
        private readonly float _speed;
        private readonly float _limit;

        public MoveCalculator(float speed, float limit)
        {
            _speed = speed;
            _limit = limit;
        }

        public Vector2 Velocity(Vector2 direction)
        {
            return direction * _speed;
        }

        public bool IsOverSpeedLimit(Vector2 velocity)
        {
            return velocity.magnitude > _limit;
        }
    }
}