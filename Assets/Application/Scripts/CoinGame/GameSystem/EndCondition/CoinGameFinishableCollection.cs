using System;
using System.Collections.Generic;

namespace CoinGame.GameSystems.EndCondition
{
    /// <summary>
    /// ゲームの終了条件を管理するクラス
    /// </summary>
    public class CoinGameFinishableCollection
    {
        private readonly List<ICoinGameFinishable> _finishables;

        public CoinGameFinishableCollection(List<ICoinGameFinishable> finishables)
        {
            _finishables = finishables;
        }

        /// <summary>
        /// 終了条件をゲームに適用させる
        /// </summary>
        /// <param name="finishedCallBack"></param>
        public void ApplyFinishRule(Action<GameResultType> finishedCallBack)
        {
            foreach (var coinGameFinishable in _finishables)
            {
                coinGameFinishable.StartObserve(finishedCallBack);
            }
        }
    }
}