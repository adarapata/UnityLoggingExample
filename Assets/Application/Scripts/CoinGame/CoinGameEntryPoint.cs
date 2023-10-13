using CoinGame.GameSystems;
using UnityEngine;

namespace CoinGame
{
    /// <summary>
    /// コインゲームの処理の開始地点
    /// </summary>
    public class CoinGameEntryPoint
    {
        private readonly GameSystem _gameSystem;
        private readonly Vector3 _spawnPosition;

        public CoinGameEntryPoint(GameSystem gameSystem, Vector3 spawnPosition)
        {
            _gameSystem = gameSystem;
            _spawnPosition = spawnPosition;
        }
        
        public void Initialize()
        {
            // プレイヤーの初期位置を渡してゲームを初期化
            _gameSystem.Initialize(_spawnPosition);
        }

        public void Tick()
        {
            // 毎フレームコインゲームのアップデート
            _gameSystem.Update();
        }
    }
}