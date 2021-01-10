using System.Collections;
using System.Collections.Generic;
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
                _score = value;
                GameplayManager.uiManager.SetScore(value);
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
                Score++;
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