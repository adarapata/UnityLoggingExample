using System;
using CoinGame.GameSystems.Data;
using CoinGame.GameSystems.Presentation;
using CoinGame.Utility;

namespace CoinGame.GameSystems
{
    public class GameTimer
    {
        private readonly CountDownTimer _timer;
        private readonly ITimerPresentation _presentation;
        private readonly GameSetting _gameSetting;

        public event Action TimeOver
        {
            add => _timer.TimeOver += value;
            remove => _timer.TimeOver -= value;
        }

        public GameTimer(ITimerPresentation timerPresentation, GameSetting setting)
        {
            _timer = new CountDownTimer();
            _presentation = timerPresentation;
            _gameSetting = setting;
        }

        public void StartTimer()
        {
            _timer.Start(_gameSetting.time);
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        public void Tick()
        {
            _timer.Tick();
            if (_timer.IsPlaying)
            {
                _presentation.ShowCurrentTime(_timer.CurrentTime);
            }
        }
    }
}