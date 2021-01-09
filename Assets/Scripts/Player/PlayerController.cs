using System;
using System.Collections;
using Misc;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject playerView;
        [SerializeField] private TeleportingFantom fantom;
        [SerializeField] private Rigidbody2D rb;

        private float TeleportingDistance => heldTime * Profile.accelerationMultiplier;

        private float heldTime = 0;

        private void Awake()
        {
            StartCoroutine(InputCoroutine());
        }

        private IEnumerator InputCoroutine()
        {
            while (true)
            {
                if (Input.GetMouseButton(0))
                {
                    var targetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                    //targetX = Mathf.Clamp(targetX, )
                    
                    playerView.transform.position = ClampToScreenBounds(new Vector3(
                        targetX,
                        playerView.transform.position.y,
                        playerView.transform.position.z
                    ), 0.5f);
                    Debug.Log(Camera.main.WorldToScreenPoint(playerView.transform.position));
                    heldTime += Time.deltaTime;
                }
                else
                {
                    if (heldTime > 0)
                    {
                        Teleport();
                        heldTime = 0f;
                    }
                }

                fantom.SetDistance(TeleportingDistance);

                yield return null;
            }
        }

        private void Teleport()
        {
            Debug.Log($"Teleporting to {playerView.transform.position.y + heldTime * Profile.accelerationMultiplier}");
            playerView.transform.position = new Vector3(
                playerView.transform.position.x,
                playerView.transform.position.y + heldTime * Profile.accelerationMultiplier,
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
    }
}