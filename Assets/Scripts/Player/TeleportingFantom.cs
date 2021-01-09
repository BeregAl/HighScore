using Shapes;
using UnityEngine;

namespace Player
{
    public class TeleportingFantom : MonoBehaviour
    {
        [SerializeField] private Line line;
        [SerializeField] private Disc disc;

        public void SetDistance(float distance)
        {
            disc.transform.localPosition = Vector3.up * distance;
            line.End = new Vector3(0, (Mathf.Clamp(distance - disc.Radius, 0, distance - disc.Radius)), 0);
        }
    }
}