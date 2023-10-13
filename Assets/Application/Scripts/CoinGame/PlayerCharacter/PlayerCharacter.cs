using System;
using CoinGame.GameSystems;
using CoinGame.GameSystems.Actors;
using CoinGame.GameSystems.Signals;
using UnityEngine;

namespace CoinGame.PlayerCharacters
{
    public class PlayerCharacter : IPlayableActor, IDisposable
    {
        private Mover _mover;
        private Jumper _jumper;
        private IEmotePresentation _emotePresentation;
        private readonly PlayerCharacterBehaviour.Factory _behaviourFactory;
        private readonly PlayerCharacterParameter _parameter;
        private readonly SignalBus<GameStartSignal> _gameStartSignal;
        private readonly SignalBus<GameEndSignal> _gameEndSignal;
        private bool CanInput { get; set; } = false;

        public PlayerCharacter(PlayerCharacterBehaviour.Factory factory, PlayerCharacterParameter parameter, SignalBus<GameStartSignal> gameStartSignal, SignalBus<GameEndSignal> gameEndSignal)
        {
            _parameter = parameter;
            _behaviourFactory = factory;
            _gameStartSignal = gameStartSignal;
            _gameEndSignal = gameEndSignal;
            _gameStartSignal.Subscribe(OnGameStart);
            _gameEndSignal.Subscribe(OnGameEnd);
        }

        private void OnGameStart(GameStartSignal signal) => CanInput = true;

        private void OnGameEnd(GameEndSignal signal)
        {
            CanInput = false;
            if (signal.ResultType == GameResultType.Win)
            {
                _emotePresentation.ShowWinEmote();
            }
            else
            {
                _emotePresentation.ShowLoseEmote();
            }
        }

        public void Dispose()
        {
            _gameStartSignal.Unsubscribe(OnGameStart);
            _gameEndSignal.Unsubscribe(OnGameEnd);
        }

        public void Initialize(Vector3 position)
        {
            // プレイヤーのインスタンスを生成する
            var behaviour = _behaviourFactory.Create();
            behaviour.Construct(position, _parameter);
            _mover = new Mover(behaviour, _parameter);
            _jumper = new Jumper(behaviour, _parameter.jump);
            _emotePresentation = behaviour;
        }

        public void UpdateActor()
        {
            if (!CanInput)
            {
                return;
            }
            
            // キー入力から左右への移動を行う
            var axis = Input.GetAxis("Horizontal");
            _mover.Acceleration(Vector2.right * axis);

            if (Input.GetButtonDown("Jump"))
            {
                _jumper.Jump();
            }
        }
    }
    
    public interface IMovePresentation
    {
        void Move(Vector2 velocity);
        float CurrentSpeed { get; }
    }
    
    public interface IJumpPresentation
    {
        void Jump(float jumpSpeed);
        bool IsGround { get; }
    }
    
    public interface IEmotePresentation
    {
        void ShowWinEmote();
        void ShowLoseEmote();
    }
}
