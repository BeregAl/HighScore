using Shapes;
using UnityEngine;

namespace Player
{
    public class TeleportingFantom : MonoBehaviour
    {
        [SerializeField] private Line line;
        [SerializeField] private Disc disc;

        private bool fantomVisible;

        public bool FantomVisible
        {
            get { return fantomVisible; }
            set
            {
                if (fantomVisible != value)
                {
                    fantomVisible = value;
                    line.enabled = value;
                    disc.enabled = value;
                    if (!value)
                    {
                        disc.DashOffset = 0f;
                        line.DashOffset = 0f;
                    }
                }
            }
        }


        public void SetDistance(float distance)
        {
            FantomVisible = true;
            disc.transform.localPosition = Vector3.up * distance;
            line.End = new Vector3(0, (Mathf.Clamp(distance - disc.Radius, 0, distance - disc.Radius)), 0);
            disc.DashOffset += Time.deltaTime;
            line.DashOffset += Time.deltaTime*2;
        }
    }
}