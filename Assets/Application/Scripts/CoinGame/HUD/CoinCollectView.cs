using CoinGame.GameSystems.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace CoinGame.HUD
{
    public class CoinCollectView : MonoBehaviour, ICoinCountPresentation
    {
        [SerializeField] private Text _countText = null;

        /// <summary>
        /// コインの取得枚数を画面に表示する
        /// </summary>
        /// <param name="current"></param>
        /// <param name="total"></param>
        public void ShowCoinCount(int current, int total)
        {
            _countText.text = $"{current}/{total}";
        }
    }
}