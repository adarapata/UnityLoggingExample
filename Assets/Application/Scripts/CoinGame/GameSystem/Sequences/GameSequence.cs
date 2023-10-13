using System.Collections;
using System.Collections.Generic;
using CoinGame.GameSystems.EndCondition;
using CoinGame.GameSystems.Presentation;
using CoinGame.GameSystems.Signals;
using Unity.Logging;
using UnityEngine;

namespace CoinGame.GameSystems.Sequences
{
    public class GameSequence
    {
        private readonly IGameStartPresentation _presentation;
        private readonly IGameEndPresentation _endPresentation;
        private readonly GameTimer _gameTimer;
        private readonly CoinGameFinishableCollection _finishableCollection;
        private readonly SignalBus<GameStartSignal> _gameStartSignal;
        private readonly SignalBus<GameEndSignal> _gameEndSignal;
        private readonly CoinCounter _coinCounter;

        private GameResultType _resultType = GameResultType.None;

        public GameSequence(IGameStartPresentation startPresentation,
            GameTimer timer, 
            List<ICoinGameFinishable> collection,
            IGameEndPresentation endPresentation, 
            SignalBus<GameStartSignal> gameStartSignal,
            SignalBus<GameEndSignal> gameEndSignal,
            CoinCounter coinCounter)
        {
            _presentation = startPresentation;
            _endPresentation = endPresentation;
            _gameTimer = timer;
            _finishableCollection = new CoinGameFinishableCollection(collection);
            _coinCounter = coinCounter;
            _gameStartSignal = gameStartSignal;
            _gameEndSignal = gameEndSignal;
        }
        
        public IEnumerator PlaySequence()
        {
            yield return PlayStartSequence();
            yield return new WaitWhile(() => _resultType == GameResultType.None);
            PlayEndSequence();
        }

        private IEnumerator PlayStartSequence()
        {
            yield return _presentation.ShowGameStart();
            _gameTimer.StartTimer();
            _coinCounter.CountStart();
            _finishableCollection.ApplyFinishRule(OnGameFinish);
            Log.Info("Game Start !");
            _gameStartSignal.Fire(new GameStartSignal()); // ゲーム開始したことを通知する
        }

        private void PlayEndSequence()
        {
            _gameTimer.StopTimer();
            if (_resultType == GameResultType.Win)
            {
                _endPresentation.ShowGameClear();
            }
            else
            {
                _endPresentation.ShowGameOver();
            }
            Log.Info("Game End ! Result {0}", _resultType);
            _gameEndSignal.Fire(new GameEndSignal(_resultType));
        }

        private void OnGameFinish(GameResultType obj)
        {
            if (_resultType != GameResultType.None)
            {
                Log.Warning("Game Finish is already called. {0}", obj);
            }
            _resultType = obj;
        }
    }
}