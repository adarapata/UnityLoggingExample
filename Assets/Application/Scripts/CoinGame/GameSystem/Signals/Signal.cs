using CoinGame.GameSystems.Actors;

namespace CoinGame.GameSystems.Signals
{
    /// <summary>
    /// コインを取得したことを伝えるシグナル
    /// </summary>
    public class CoinGetSignal
    {
        public readonly ICoin GetCoin;

        public CoinGetSignal(ICoin getCoin) => GetCoin = getCoin;
    }
    
    /// <summary>
    /// ゲームが開始されたことを伝えるシグナル
    /// </summary>
    public class GameStartSignal { }

    /// <summary>
    /// ゲームが終了したことを伝えるシグナル
    /// </summary>
    public class GameEndSignal
    {
        public GameResultType ResultType { get; }

        public GameEndSignal(GameResultType resultType)
        {
            ResultType = resultType;
        }
    }
}