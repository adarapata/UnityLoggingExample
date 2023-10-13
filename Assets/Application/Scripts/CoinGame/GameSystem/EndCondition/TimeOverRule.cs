using System;

namespace CoinGame.GameSystems.EndCondition
{
    /// <summary>
    /// 制限時間をオーバーしたらゲームを終了するルール
    /// </summary>
    public class TimeOverRule : ICoinGameFinishable
    {
        private readonly GameTimer _gameTimer;
        private Action<GameResultType> _finishedCallback;

        public TimeOverRule(GameTimer timer)
        {
            _gameTimer = timer;
        }
        
        public void StartObserve(Action<GameResultType> finishedCallback)
        {
            // タイマーを購読して時間切れになったら通知する
            _gameTimer.TimeOver += OnTimeOver;
            _finishedCallback = finishedCallback;
        }

        private void OnTimeOver() => _finishedCallback?.Invoke(GameResultType.Lose);
    }
}