using CoinGame.GameSystems.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace CoinGame.HUD
{
    public class GameEndView : MonoBehaviour, IGameEndPresentation
    {
        [SerializeField] private Text _text;

        public void ShowGameClear()
        {
            _text.text = "GAME CLEAR!";
        }

        public void ShowGameOver()
        {
            _text.text = "GAME OVER..";
        }
    }
}