using System;
using UnityEngine;

namespace CoinGame.Utility
{
    public class CountDownTimer
    {
        private float _currentTime;
        private float _duration;
        private bool _isPlaying;

        public bool IsPlaying => _isPlaying;

        public event Action TimeOver;

        public void Start(float duration)
        {
            _duration = duration;
            _currentTime = 0;
            _isPlaying = true;
        }

        public void Stop()
        {
            _isPlaying = false;
        }

        public void Tick()
        {
            if (!_isPlaying)
            {
                return;
            }
            
            _currentTime += Time.deltaTime;

            if (_currentTime >= _duration)
            {
                TimeOver?.Invoke();
                Stop();
            }
        }

        public float CurrentTime => _duration - _currentTime;
    }
}