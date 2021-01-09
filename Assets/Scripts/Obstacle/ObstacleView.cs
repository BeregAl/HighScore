using Shapes;
using UnityEngine;

namespace Obstacle
{
    public class ObstacleView : MonoBehaviour
    {
        [SerializeField] private Rectangle rect;
        [SerializeField] private Line line;
        [SerializeField] private BoxCollider2D platformCollider;
        [SerializeField] private BoxCollider2D lineCollider;

        public void Init(float width = 1f, float xPos = 0)
        {
            rect.Width = width;
            line.End = new Vector3(width, 0, 0);
            
            platformCollider.offset = new Vector2(width / 2, 0.75f);
            platformCollider.size = new Vector2(width, 0.5f);
            
            lineCollider.offset = new Vector2(width / 2, 0f);
            lineCollider.size = new Vector2(width, 0.1f);
            transform.localPosition = new Vector3(xPos, Camera.main.orthographicSize + 1f, 0f);
        }
        
    }
}