using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Vector2[] points;

    private void Awake()
    {
        lr = this.GetComponent<LineRenderer>();
    }

    public void SetupLine(Vector2[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }
    
    void Update()
    {
        for(int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i]);
        }
    }
}
