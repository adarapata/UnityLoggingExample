using System;
using UnityEngine;

namespace CoinGame.GameSystems.Data
{
    [CreateAssetMenu]
    public class GameSettingContainer : ScriptableObject
    {
        [SerializeField] private GameSetting _setting;

        public GameSetting Setting => _setting;
    }

    [Serializable]
    public class GameSetting
    {
        public float time;
    }
}