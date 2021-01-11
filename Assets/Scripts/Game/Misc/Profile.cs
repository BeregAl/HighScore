using System;
using UnityEngine;

namespace Misc
{
    public class Profile : MonoBehaviour
    {
        public static Profile instance;
        
        public GameSetting acceleration = new GameSetting(5f, 10);
        public GameSetting maxTeleportingDistance = new GameSetting(3f, 9f);
        public GameSetting fallingSpeed =  new GameSetting(3.5f, 7f);
        public GameSetting obstacleSpawningCooldown = new GameSetting(2f, float.MaxValue, 0.5f);
        public GameSetting powerUpsSpawningProbability = new GameSetting(0.1f, 0.3f);
        public GameSetting horizontalSpeed = new GameSetting(5f, 9f);
        public float scoreMultiplier = 1f;
        public bool SpawnAsShit;

        private void Awake()
        {
            instance = this;
            if (SpawnAsShit)
            {
                obstacleSpawningCooldown.Set(0.3f);
                fallingSpeed.Set(3f);
            }
        }
    }

    public class GameSetting
    {
        public float Value => Mathf.Clamp((baseValue + modValue) * (baseMultiplier + modMultiplier), minValue, maxValue);//TODO: нормальная математика
        private float baseValue;
        private float modValue = 0f;
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

        public void AddValue(float value)
        {
            modValue += value;
        }
    }
}