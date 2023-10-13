using System;

namespace CoinGame.GameSystems
{
    /// <summary>
    /// ゲームの終わらせ方を知っているインタフェース
    /// </summary>
    public interface ICoinGameFinishable
    {
        /// <summary>
        /// ゲーム終了条件を満たしたか監視する
        /// </summary>
        /// <param name="finishedCallback"></param>
        void StartObserve(Action<GameResultType> finishedCallback);
    }

    public enum GameResultType
    {
        None,
        Win,
        Lose
    }
}