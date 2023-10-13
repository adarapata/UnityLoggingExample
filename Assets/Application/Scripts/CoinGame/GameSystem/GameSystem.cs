using System.Collections;
using CoinGame.GameSystems.Actors;
using CoinGame.GameSystems.Sequences;
using CoinGame.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CoinGame.GameSystems
{
    public class GameSystem
    {
        private readonly GameSequence _sequence;
        private readonly GameRetrySequence _retrySequence;
        private readonly IPlayableActor _playableActor;
        private readonly GameTimer _gameTimer;
        
        public GameSystem(GameSequence sequence, GameRetrySequence retrySequence, IPlayableActor playableActor, GameTimer gameTimer)
        {
            _sequence = sequence;
            _retrySequence = retrySequence;
            _playableActor = playableActor;
            _gameTimer = gameTimer;
        }

        /// <summary>
        /// コインゲームの開始から終了までを順番に呼び出す
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartSequence()
        {
            yield return _sequence.PlaySequence();
            yield return _retrySequence.PlaySequence();
            SceneManager.LoadScene("Main");
        }

        public void Initialize(Vector3 spawnPosition)
        {
            _playableActor.Initialize(spawnPosition);
            // ゲームシーケンスをコルーチンで呼び出す
            CoroutineHelper.StartCoroutineMethod(StartSequence());
        }

        public void Update()
        {
            _playableActor.UpdateActor();
            _gameTimer.Tick();
        }
    }
}