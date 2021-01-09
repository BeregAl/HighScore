using System;
using System.Collections;
using Misc;
using UnityEngine;

namespace Managers
{
    public class DifficultyManager : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(DifficultyCoroutine());
        }

        private IEnumerator DifficultyCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                Profile.obstacleSpawningCooldown.AddValue(-0.1f);
                Profile.fallingSpeed.AddValue(0.1f);
            }
        }
    }
}