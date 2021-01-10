using System;
using System.Collections;
using System.Collections.Generic;
using Shapes;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private RegularPolygon polygon;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private bool clockwise = true;

    private void Update()
    {
        polygon.Angle += rotationSpeed * Time.deltaTime * (clockwise ? 1 : -1);
    }
}
