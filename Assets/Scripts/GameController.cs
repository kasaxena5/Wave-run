using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private LineController line;
    private List<Vector2> points;
    [SerializeField] private float width = 10;
    [SerializeField] private float stepSize = 1;
    [SerializeField] private float xOffset = 0;
    [SerializeField] private float yOffset = 0;
    [SerializeField] private float scale = 1;
    [SerializeField] private float heightScale = 3;
    [SerializeField] private float speed;

    void Start()
    {
        SetupLine();
    }

    private void SetupLine()
    {
        points = new();
        for (float i = -width; i <= width; i += stepSize)
        {
            float x = (((float)i / (float)width) * scale) + xOffset;
            float height = Mathf.PerlinNoise(x, yOffset);
            points.Add(new Vector2(i, heightScale * height));
        }
        line.SetupLine(points.ToArray());
    }

    void Update()
    {
        xOffset += (speed * Time.deltaTime);
        SetupLine();

    }
}
