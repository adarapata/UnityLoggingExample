using System.Collections;
using CoinGame.GameSystems.Presentation;
using UnityEngine;
using UnityEngine.UI;

namespace CoinGame.HUD
{
    public class GameStartView : MonoBehaviour, IGameStartPresentation
    {
        [SerializeField] private Text _startText;

        public IEnumerator ShowGameStart()
        {
            _startText.gameObject.SetActive(true);
            _startText.text = "Ready!";
            yield return new WaitForSeconds(1f);
            _startText.text = "Go!";
            yield return new WaitForSeconds(0.5f);
            _startText.gameObject.SetActive(false);
        }
    }
}