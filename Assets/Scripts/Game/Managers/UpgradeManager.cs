using Misc;
using UnityEngine;

namespace Managers
{
    public class UpgradeManager : MonoBehaviour, IManager
    {
        public void CollectUpgrade(UpdrageType type)
        {
            switch (type)
            {
                case UpdrageType.Acceleration:
                    Profile.instance.acceleration.AddMultiplier(0.1f);
                    break;
                case UpdrageType.MaxDistance:
                    Profile.instance.maxTeleportingDistance.AddMultiplier(0.1f);
                    break;
                case UpdrageType.Speed:
                    Profile.instance.horizontalSpeed.AddMultiplier(0.5f);
                    break;
            }
        }

        public void Subscribe()
        {
        }

        public void Unsubscribe()
        {
        }
    }

    public enum UpdrageType
    {
        Acceleration,
        MaxDistance,
        Speed,
    }
}