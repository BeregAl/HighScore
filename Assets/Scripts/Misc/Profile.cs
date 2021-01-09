using System;
using UnityEngine;

namespace Misc
{
    public class Profile : MonoBehaviour
    {
        public static float accelerationMultiplier = 3f;
        public static float maxTeleportingDistance = 5f;
        public static float fallingSpeed = 1f;
        public static float obstacleSpawningCooldown = 5f;
        public static float scoreMultiplier = 1f;
        public bool SpawnAsShit;

        private void Awake()
        {
            if (SpawnAsShit)
            {
                obstacleSpawningCooldown = 0.3f;
                fallingSpeed = 3f;
            }
            
        }
    }
    
    public class 
}