using System.Collections;
using CoinGame.GameSystems.Presentation;
using UnityEngine;

namespace CoinGame.GameSystems.Sequences
{
    public class GameRetrySequence
    {
        private readonly IRetryPresentation _retryPresentation;

        public GameRetrySequence(IRetryPresentation presentation)
        {
            _retryPresentation = presentation;
        }
        
        public IEnumerator PlaySequence()
        {
            _retryPresentation.ShowRetryMessage();
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        }
    }
}