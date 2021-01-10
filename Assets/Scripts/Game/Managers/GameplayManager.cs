using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GameplayManager : MonoBehaviour
    {
        public event Action GameOverEvent;
        public static GameplayManager gameplay;
        public static Spawner spawner;
        public static UpgradeManager upgrade;
        public static ScoreManager scoreManager;
        public static UiManager uiManager;
        private List<IManager> managers = new List<IManager>();
        
        
        [SerializeField] private GameObject loseSplash;

        private void Awake()
        {
            gameplay = this;
            managers.Add(spawner = GetComponent<Spawner>());
            managers.Add(upgrade = GetComponent<UpgradeManager>());
            managers.Add(scoreManager = GetComponent<ScoreManager>());
            managers.Add(uiManager = GetComponent<UiManager>());
            
            foreach (var manager in managers)
            {
                manager.Subscribe();
            }
        }

        public void Lose()
        {
            loseSplash.SetActive(true);
            GameOverEvent?.Invoke();
            GameManager.instance.playfabManager.SendLeaderboard((int)scoreManager.Score);
            GameManager.instance.uiManager.leaderboardDialog.ShowDialog((int)scoreManager.Score);
        }

        public void OnDestroy()
        {
            foreach (var manager in managers)
            {
                manager.Unsubscribe();
            }
        }
    }
}