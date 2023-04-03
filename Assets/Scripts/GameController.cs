using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private LineController line;
    private List<Vector2> points;
    public float speed;

    void Start()
    {
        points = new();
        for(int i = 0; i < 10; i++)
        {
            points.Add(new Vector2(i, 10 * i));
        }
        line.SetupLine(points.ToArray());
    }

    void Update()
    {
        for(int i= 0; i < 10; i++)
        {
            points[i] = new Vector2(points[i].x - speed, points[i].y);
        }
        line.SetupLine(points.ToArray());

    }
}
