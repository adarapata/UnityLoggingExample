using System;
using UnityEngine;

namespace CoinGame.PlayerCharacters
{
    [CreateAssetMenu]
    public class PlayerCharacterParameterContainer : ScriptableObject
    {
        [SerializeField] private PlayerCharacterParameter _parameter;

        public PlayerCharacterParameter Parameter => _parameter;
    }

    [Serializable]
    public class PlayerCharacterParameter
    {
        public float speed;
        public float speedLimit;
        public float jump;
    }
}

