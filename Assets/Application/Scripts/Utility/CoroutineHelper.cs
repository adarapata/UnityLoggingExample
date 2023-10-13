using System.Collections;
using UnityEngine;

namespace CoinGame.Utility
{
    public class CoroutineHelper : MonoBehaviour
    {
        private static CoroutineHelper _instance;

        private static CoroutineHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject(nameof(CoroutineHelper)).AddComponent<CoroutineHelper>();
                }

                return _instance;
            }
        }
        
        public static Coroutine StartCoroutineMethod(IEnumerator routine)
        {
            return Instance.StartCoroutine(routine);
        }
    }
}