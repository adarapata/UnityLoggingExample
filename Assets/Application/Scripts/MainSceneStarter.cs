using System;
using System.Collections.Generic;
using System.Linq;
using CoinGame;
using CoinGame.Coin;
using CoinGame.GameSystems;
using CoinGame.GameSystems.Actors;
using CoinGame.GameSystems.Data;
using CoinGame.GameSystems.EndCondition;
using CoinGame.GameSystems.Sequences;
using CoinGame.GameSystems.Signals;
using CoinGame.HUD;
using CoinGame.PlayerCharacters;
using Unity.Logging;
using Unity.Logging.Sinks;
using Unity.Services.Core;
using UnityEngine;

namespace CoinGame
{
    public class MainSceneStarter : MonoBehaviour
    {
        [SerializeField] private PlayerCharacterBehaviour playerPrefab;
        [SerializeField] private PlayerCharacterParameterContainer _parameterContainer;
        [SerializeField] private GameSettingContainer _gameSetting;
        [SerializeField] private Transform _playerSpawnPosition;

        [SerializeField] private CoinCollectView _coinCollectView;
        [SerializeField] private GameEndView _gameEndView;
        [SerializeField] private GameStartView _gameStartView;
        [SerializeField] private GameTimeView _gameTimeView;
        [SerializeField] private GameRetryView _gameRetryView;
        [SerializeField] private List<CoinBehaviour> _coinBehaviour;

        private CoinGameEntryPoint _entryPoint;

        private void Awake()
        {
            InitializeLogging();
        }

        async void Start()
        {
            try
            {
                await UnityServices.InitializeAsync();
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
            }

            var gameStartSignalBus = new SignalBus<GameStartSignal>();
            var gameEndSignalBus = new SignalBus<GameEndSignal>();
            var coinGetSignalBus = new SignalBus<CoinGetSignal>();

            var playableCharacter = new PlayerCharacter(new PlayerCharacterBehaviour.Factory(playerPrefab), _parameterContainer.Parameter, gameStartSignalBus, gameEndSignalBus);
            
            var gameTimer = new GameTimer(_gameTimeView, _gameSetting.Setting);

            var allCoins = _coinBehaviour.Cast<ICoin>().ToList();
            var coinCounter = new CoinCounter(allCoins, coinGetSignalBus, _coinCollectView);
            foreach (var coinBehaviour in _coinBehaviour)
            {
                coinBehaviour.Construct(coinGetSignalBus);
            }

            var coinGameFinishRules = new List<ICoinGameFinishable>()
            {
                new CoinCollectRule(coinCounter),
                new TimeOverRule(gameTimer)
            };

            var gameSequence = new GameSequence(_gameStartView, gameTimer, coinGameFinishRules, _gameEndView, gameStartSignalBus, gameEndSignalBus, coinCounter);
            var gameRetrySequence = new GameRetrySequence(_gameRetryView);
            var gameSystem = new GameSystem(gameSequence, gameRetrySequence, playableCharacter, gameTimer);
            _entryPoint = new CoinGameEntryPoint(gameSystem, _playerSpawnPosition.position);
            _entryPoint.Initialize();
        }
        
        private void Update()
        {
            _entryPoint.Tick();
            
            // 意図的にExceptionを発生させる
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.LogException(new Exception("Debug Exception"));
            }
        }

        private void InitializeLogging()
        {
            var config = new LoggerConfig()
                .MinimumLevel.Debug()
                .OutputTemplate("{Timestamp} - {Level} - {Message}")
                .CaptureStacktrace()
                .RedirectUnityLogs()
                .WriteTo.UnityEditorConsole();

            Log.Logger = new Unity.Logging.Logger(config);
        }
    }
}
