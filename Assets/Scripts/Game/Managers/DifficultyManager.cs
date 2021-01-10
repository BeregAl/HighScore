using System;
using System.Collections;
using Misc;
using UnityEngine;

namespace Managers
{
    public class DifficultyManager : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(DifficultyCoroutine());
        }

        private IEnumerator DifficultyCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(3f);
                Profile.instance.obstacleSpawningCooldown.AddValue(-0.1f);
                Profile.instance.fallingSpeed.AddValue(0.1f);
                Profile.instance.scoreMultiplier += 0.1f;
                Debug.Log($"Diff {Profile.instance.obstacleSpawningCooldown.Value} {Profile.instance.fallingSpeed.Value}");
            }
        }
    }
}