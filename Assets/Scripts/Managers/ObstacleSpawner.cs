using System;
using System.Collections;
using System.Collections.Generic;
using Misc;
using Obstacle;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{

    public interface IManager
    {
        void Subscribe();
        void Unsubscribe();
        
    }
    public class ObstacleSpawner : MonoBehaviour, IManager
    {
        [SerializeField] private GameObject obstaclePrefab;
        [SerializeField] private Transform obstaclesParent;

        private List<GameObject> obstacles = new List<GameObject>();
        private float LevelWidth => Camera.main.orthographicSize * 2 * Camera.main.aspect;
        private int discretionSteps = 8;

        private void Awake()
        {
            StartCoroutine(SpawningCoroutine());
            StartCoroutine(FallingCoroutine());
        }

        private IEnumerator SpawningCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Profile.obstacleSpawningCooldown.Value);
                var newObstacle = Instantiate(obstaclePrefab, obstaclesParent);
                var view = newObstacle.GetComponent<ObstacleView>();
                var obstacleWidth = Random.Range(2, discretionSteps);
                var oneUnitWidth = LevelWidth / discretionSteps;
                var obstaclePos =
                    Random.Range(0, discretionSteps + 1 - obstacleWidth);
                view.Init(obstacleWidth * oneUnitWidth, obstaclePos * oneUnitWidth - LevelWidth / 2);
                obstacles.Add(newObstacle);
                yield return null;
            }
        }

        private IEnumerator FallingCoroutine()
        {
            while (true)
            {
                foreach (var obstacle in obstacles)
                {
                    obstacle.transform.Translate(Vector3.down *
                                                 (Time.deltaTime *
                                                  Profile.fallingSpeed.Value));
                }

                for (int i = 0; i < obstacles.Count; i++)
                {
                    if (obstacles[i].transform.position.y < -Camera.main.orthographicSize - 1)
                    {
                        var obsoleteObstacle = obstacles[i];
                        obstacles.Remove(obsoleteObstacle);
                        Destroy(obsoleteObstacle);
                    }
                }

                yield return null;
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