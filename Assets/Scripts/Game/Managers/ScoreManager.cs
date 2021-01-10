using System.Collections;
using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour, IManager
    {
        public float Score
        {
            get => _score;
            set
            {
                _score = (int)value;
                GameplayManager.uiManager.SetScore(_score);
            }
        }

        private float _score = 0f;

        private WaitForSeconds _scoringDelay;

        private void Start()
        {
            _scoringDelay = new WaitForSeconds(0.5f);
            StartCoroutine(Scoring());
        }

        private IEnumerator Scoring()
        {
            while (true)
            {
                yield return _scoringDelay;
                Score += Profile.instance.scoreMultiplier;
            }
        }

        public void Subscribe()
        {
            GameplayManager.gameplay.GameOverEvent += OnGameOver;
        }
        
        private void OnGameOver()
        {
            StopAllCoroutines();
        }

        public void Unsubscribe()
        {
            GameplayManager.gameplay.GameOverEvent -= OnGameOver;
        }
    }
}