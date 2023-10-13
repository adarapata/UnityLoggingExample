using CoinGame.GameSystems.Presentation;
using UnityEngine;

namespace CoinGame.HUD
{
    public class GameRetryView : MonoBehaviour, IRetryPresentation
    {
        [SerializeField] private GameObject _retryWindow;
        public void ShowRetryMessage()
        {
            _retryWindow.SetActive(true);
        }
    }
}