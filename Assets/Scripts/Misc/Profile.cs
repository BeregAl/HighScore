using System;
using UnityEngine;

namespace Misc
{
    public class Profile : MonoBehaviour
    {
        public static GameSetting acceleration = new GameSetting(3f, 10);
        public static GameSetting maxTeleportingDistance = new GameSetting(5f, 10f);
        public static GameSetting fallingSpeed =  new GameSetting(1f, 3f);
        public static GameSetting obstacleSpawningCooldown = new GameSetting(5f, float.MaxValue, 0.3f);
        public static float scoreMultiplier = 1f;
        public bool SpawnAsShit;

        private void Awake()
        {
            if (SpawnAsShit)
            {
                obstacleSpawningCooldown.Set(0.3f);
                fallingSpeed.Set(3f);
            }
        }
    }

    public class GameSetting
    {
        public float Value => Mathf.Clamp(baseValue * (baseMultiplier + modMultiplier),0, maxValue);
        private float baseValue;
        private float maxValue;
        private float minValue;
        private float baseMultiplier = 1f;
        private float modMultiplier = 0f;

        public GameSetting(float baseValue, float maxValue = float.MaxValue, float minValue = 0)
        {
            this.baseValue = baseValue;
            this.maxValue = maxValue;
            this.minValue = minValue;
        }

        public void AddMultiplier(float value)
        {
            modMultiplier += value;
        }

        public void Set(float value, bool resetMultiplier=false)
        {
            baseValue = value;
            if (resetMultiplier)
                modMultiplier = 0f;
        }
    }
}