using System;
using System.Collections;
using System.Collections.Generic;
using Misc;
using Obstacle;
using UnityEngine;
using Upgrades;
using Random = UnityEngine.Random;

namespace Managers
{

    public interface IManager
    {
        void Subscribe();
        void Unsubscribe();
        
    }
    public class Spawner : MonoBehaviour, IManager
    {
        [SerializeField] private GameObject obstaclePrefab;
        [SerializeField] private List<GameObject> powerupPrefab;
        [SerializeField] private Transform objectsParent;

        private List<GameObject> objects = new List<GameObject>();
        private float LevelWidth => Camera.main.orthographicSize * 2 * Camera.main.aspect;
        private int discretionSteps = 8;

        private void Start()
        {
            StartCoroutine(SpawningCoroutine());
            StartCoroutine(FallingCoroutine());
        }

        private IEnumerator SpawningCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Profile.instance.obstacleSpawningCooldown.Value);

                var rand = Random.Range(0, 1f);
                Debug.Log($"Rand: {rand}");
                if (rand < Profile.instance.powerUpsSpawningProbability.Value)
                {
                    SpawnPowerup();
                }
                else
                {
                    SpawnWall();
                }
                yield return null;
            }
        }

        private void SpawnPowerup()
        {
            var powerup = Instantiate(powerupPrefab[Random.Range(0, powerupPrefab.Count)], objectsParent);
            var obstaclePos =
                Random.Range(-LevelWidth / 2 , LevelWidth/2 );
            var view = powerup.GetComponent<PowerupView>();
            view.Init(obstaclePos);
            objects.Add(powerup);
        }

        private void SpawnWall()
        {
            var newObstacle = Instantiate(obstaclePrefab, objectsParent);
            var view = newObstacle.GetComponent<ObstacleView>();
            var obstacleWidth = Random.Range(2, discretionSteps);
            var oneUnitWidth = LevelWidth / discretionSteps;
            var obstaclePos =
                Random.Range(0, discretionSteps + 1 - obstacleWidth);
            view.Init(obstacleWidth * oneUnitWidth, obstaclePos * oneUnitWidth - LevelWidth / 2);
            objects.Add(newObstacle);
        }

        private IEnumerator FallingCoroutine()
        {
            while (true)
            {
                foreach (var obstacle in objects)
                {
                    obstacle.transform.Translate(Vector3.down *
                                                 (Time.deltaTime *
                                                  Profile.instance.fallingSpeed.Value));
                }

                for (int i = 0; i < objects.Count; i++)
                {
                    if (objects[i].transform.position.y < -Camera.main.orthographicSize - 1)
                    {
                        RemoveObject(objects[i]);
                    }
                }

                yield return null;
            }
        }

        public void RemoveObject(GameObject obj)
        {
            if (objects.Contains(obj))
            {
                objects.Remove(obj);
            }
            Destroy(obj);
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