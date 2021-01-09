using System;
using System.Collections;
using System.Collections.Generic;
using Misc;
using Obstacle;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject obstaclePrefab;
        [SerializeField] private Transform obstaclesParent;

        private List<GameObject> obstacles = new List<GameObject>();
        private float LevelWidth => Camera.main.orthographicSize * Camera.main.aspect;

        private void Awake()
        {
            StartCoroutine(SpawningCoroutine());
            StartCoroutine(FallingCoroutine());
        }

        private IEnumerator SpawningCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Profile.obstacleSpawningCooldown);
                var newObstacle = Instantiate(obstaclePrefab, obstaclesParent);
                var view = newObstacle.GetComponent<ObstacleView>();
                var obstacleWidth = Random.Range(1f,4f);
                var obstaclePos = Random.Range(-LevelWidth, LevelWidth - obstacleWidth);
                view.Init(obstacleWidth, obstaclePos);
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
                    obstacle.transform.Translate(Vector3.down*Time.deltaTime*Profile.fallingSpeed); //TODO: Удалять блоки, которые слишком низко
                }

                yield return null;
            }
        }
    }
}