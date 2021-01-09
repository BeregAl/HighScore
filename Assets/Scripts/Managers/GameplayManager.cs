using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GameplayManager : MonoBehaviour
    {
        public event Action GameOverEvent;
        public static GameplayManager gameplay;
        public static ObstacleSpawner obstacleSpawner;
        private List<IManager> managers = new List<IManager>();

        private void Awake()
        {
            gameplay = this;
            managers.Add(obstacleSpawner = GetComponent<ObstacleSpawner>());
            
            foreach (var manager in managers)
            {
                manager.Subscribe();
            }
        }

        public void Lose()
        {
            GameOverEvent?.Invoke();
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