using Managers;
using UnityEngine;

namespace Upgrades
{
    public class PowerupView : MonoBehaviour
    {
        public UpdrageType type;

        public void Apply()
        {
            GameplayManager.upgrade.CollectUpgrade(type);
            GameplayManager.spawner.RemoveObject(gameObject);
            
            GameManager.instance.audioManager.PlayPowerUp();
            GameplayManager.scoreManager.Score += 3;
        }

        public void Init(float xPos)
        {
            transform.localPosition = new Vector3(xPos, Camera.main.orthographicSize + 1f, 0f);
        }
    }
}