using System;
using System.Collections;
using Managers;
using Misc;
using UnityEngine;
using Upgrades;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject playerView;
        [SerializeField] private TeleportingFantom fantom;
        [SerializeField] private Rigidbody2D rb;

        private float TeleportingDistance => heldTime * Profile.acceleration.Value;

        private float heldTime = 0;

        private void Start()
        {
            StartCoroutine(InputCoroutine());
            GameplayManager.gameplay.GameOverEvent += OnGameOver;
        }

        private void OnGameOver()
        {
            StopAllCoroutines();
        }

        private IEnumerator InputCoroutine()
        {
            var teleportingDistance = 0f;
            while (true)
            {
                teleportingDistance =
                    Mathf.Clamp(TeleportingDistance, TeleportingDistance, Profile.maxTeleportingDistance.Value);
                fantom.SetDistance(teleportingDistance);
                if (Input.GetMouseButton(0))
                {
                    var targetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                    //targetX = Mathf.Clamp(targetX, )
                    
                    playerView.transform.position = ClampToScreenBounds(new Vector3(
                        targetX,
                        playerView.transform.position.y,
                        playerView.transform.position.z
                    ), 0.5f);
                    heldTime += Time.deltaTime;
                }
                else
                {
                    if (heldTime > 0)
                    {
                        fantom.FantomVisible = false;
                        Teleport(teleportingDistance);
                        heldTime = 0f;
                    }
                }


                yield return null;
            }
        }

        private void Teleport(float distance)
        {
            playerView.transform.position = new Vector3(
                playerView.transform.position.x,
                playerView.transform.position.y + distance,
                playerView.transform.position.z
            );
            rb.velocity = Vector2.zero;
        }

        public Vector3 ClampToScreenBounds(Vector3 input, float radius)
        {
            float targetX;
            var rad = radius * (Screen.height / Camera.main.orthographicSize / 2);
            var pos = Camera.main.WorldToScreenPoint(input);
            if (pos.x < rad)
            {
                targetX = rad;
            }
            else if (pos.x > Screen.width - rad)
            {
                targetX = Screen.width - rad;
            }
            else
            {
                targetX = pos.x;
            }
            return Camera.main.ScreenToWorldPoint(new Vector3(targetX, pos.y, pos.z));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Obstacle"))
            {
                GameplayManager.gameplay.Lose();
                GameManager.instance.audioManager.PlayLose();
                Debug.Log("GameOver suka");
            }
            else if (other.CompareTag("Powerup"))
            {
                other.gameObject.GetComponent<PowerupView>().Apply();
            }
        }
    }
}