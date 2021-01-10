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

        private float desiredPosX;

        private float TeleportingDistance => heldTime * Profile.instance.acceleration.Value;

        private float heldTime = 0;

        private void Start()
        {
            StartCoroutine(InputCoroutine());
            StartCoroutine(MovingCoroutine());
            GameplayManager.gameplay.GameOverEvent += OnGameOver;
        }

        private IEnumerator MovingCoroutine()
        {
            while (true)
            {
                var targetTranslationX = Vector3.right * ((desiredPosX - playerView.transform.position.x) * Profile.instance.horizontalSpeed.Value);
                
                playerView.transform.position =  ClampToScreenBounds(new Vector3(
                    playerView.transform.position.x + targetTranslationX.x,
                    playerView.transform.position.y,
                    playerView.transform.position.z
                ), 0.5f);
                yield return new WaitForEndOfFrame();
            }
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
                    Mathf.Clamp(TeleportingDistance, TeleportingDistance, Profile.instance.maxTeleportingDistance.Value);
                fantom.SetDistance(teleportingDistance);
                if (Input.GetMouseButton(0))
                {
                    desiredPosX =  Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
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

            if (pos.y > Screen.height - rad)
            {
                pos.y = Screen.height - rad;
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