using UnityEngine;

namespace CoinGame.PlayerCharacters
{
    public class Mover
    {
        private readonly MoveCalculator _calculator;
        private readonly IMovePresentation _presentation;
        public Mover(IMovePresentation presentation, PlayerCharacterParameter parameter)
        {
            _calculator = new MoveCalculator(parameter.speed, parameter.speedLimit);
            _presentation = presentation;
        }

        public void Acceleration(Vector2 direction)
        {
            if (!_calculator.IsOverSpeedLimit(Vector2.right * _presentation.CurrentSpeed))
            {
                _presentation.Move(_calculator.Velocity(direction));
            }
        }
    }
}