using System;
using UnityEngine;

namespace Misc
{
    public class ScreenEdgeCollider : MonoBehaviour
    {
        private Camera camera;
        private void Awake()
        {
            camera = Camera.main;
            GenerateCollidersAcrossScreen();
        }

        private void GenerateCollidersAcrossScreen()
        {
            Vector2 lDCorner = camera.ViewportToWorldPoint(new Vector3(0, 0f, camera.nearClipPlane));
            Vector2 rUCorner = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));
            Vector2[] colliderpoints;

            var newObject = new GameObject();
            newObject.transform.parent = transform;
            newObject.name = "upperEdge";
            EdgeCollider2D upperEdge = newObject.AddComponent<EdgeCollider2D>();
            colliderpoints = upperEdge.points;
            colliderpoints[0] = new Vector2(lDCorner.x, rUCorner.y);
            colliderpoints[1] = new Vector2(rUCorner.x, rUCorner.y);
            upperEdge.points = colliderpoints;

            newObject = new GameObject();
            newObject.transform.parent = transform;
            newObject.name = "lowerEdge";
            EdgeCollider2D lowerEdge = newObject.AddComponent<EdgeCollider2D>();
            colliderpoints = lowerEdge.points;
            colliderpoints[0] = new Vector2(lDCorner.x, lDCorner.y);
            colliderpoints[1] = new Vector2(rUCorner.x, lDCorner.y);
            lowerEdge.points = colliderpoints;

            newObject = new GameObject();
            newObject.transform.parent = transform;
            newObject.name = "leftEdge";
            EdgeCollider2D leftEdge = newObject.AddComponent<EdgeCollider2D>();
            colliderpoints = leftEdge.points;
            colliderpoints[0] = new Vector2(lDCorner.x, lDCorner.y);
            colliderpoints[1] = new Vector2(lDCorner.x, rUCorner.y);
            leftEdge.points = colliderpoints;

            newObject = new GameObject();
            newObject.transform.parent = transform;
            newObject.name = "rightEdge";
            EdgeCollider2D rightEdge = newObject.AddComponent<EdgeCollider2D>();
            colliderpoints = rightEdge.points;
            colliderpoints[0] = new Vector2(rUCorner.x, rUCorner.y);
            colliderpoints[1] = new Vector2(rUCorner.x, lDCorner.y);
            rightEdge.points = colliderpoints;
        }
    }
}