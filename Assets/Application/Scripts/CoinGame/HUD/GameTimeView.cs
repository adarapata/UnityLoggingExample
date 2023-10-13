using CoinGame.GameSystems.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace CoinGame.HUD
{
    public class GameTimeView : MonoBehaviour, ITimerPresentation
    {
        [SerializeField] private Text _timerText;

        /// <summary>
        /// 現在時刻を画面に表示する
        /// </summary>
        /// <param name="currentTime"></param>
        public void ShowCurrentTime(float currentTime)
        {
            _timerText.text = currentTime.ToString("F2");
        }
    }
}