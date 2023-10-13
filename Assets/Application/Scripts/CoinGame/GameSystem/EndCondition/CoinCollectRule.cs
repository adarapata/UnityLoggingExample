using System;

namespace CoinGame.GameSystems.EndCondition
{
    /// <summary>
    /// コインをすべて集めたらゲームを終了させるルール
    /// </summary>
    public class CoinCollectRule : ICoinGameFinishable
    {
        private readonly CoinCounter _coinCounter;

        public CoinCollectRule(CoinCounter coinCounter)
        {
            _coinCounter = coinCounter;
        }

        public void StartObserve(Action<GameResultType> finishedCallback)
        {
            // コインをすべて集めたらゲームに勝利したことを通知する
            _coinCounter.CoinCollected += () => finishedCallback(GameResultType.Win);
        }
    }
}
