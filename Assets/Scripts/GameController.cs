using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private LineController upperLine;
    [SerializeField] private LineController lowerLine;

    [SerializeField] private ObstacleController obstaclePrefab;
    private Queue<ObstacleController> obstacles;
    
    private List<Vector2> upperLinePoints;
    private List<Vector2> lowerLinePoints;

    [SerializeField] private float width = 10f;
    [SerializeField] private float stepSize = 0.5f;
    
    [SerializeField] private float upperLineXOffset = 0f;
    [SerializeField] private float lowerLineXOffset = 10f;

    [SerializeField] private float upperLineHeightOffset = 0f;
    [SerializeField] private float lowerLineHeightOffset = -1f;

    [SerializeField] private float scale = 1f;
    [SerializeField] private float heightScale = 5f;
    [SerializeField] private float speed = 1f;

    [SerializeField] private float obstacleWaitTime = 2;

    private void Awake()
    {
        obstacles = new Queue<ObstacleController>();
    }

    void Start()
    {
        SetupLines();
        StartCoroutine(SpawnObstacles());
    }

    void SetupPlayer()
    {

    }

    IEnumerator SpawnObstacles()
    {
        yield return new WaitForSeconds(obstacleWaitTime);
        while(true)
        {
            ObstacleController obstacle = Instantiate(obstaclePrefab);
            obstacle.position = width + obstacle.width;
            obstacle.xOffset = upperLineXOffset;
            obstacle.heightOffset = upperLineHeightOffset;
            obstacles.Enqueue(obstacle);
            yield return new WaitForSeconds(obstacleWaitTime);
        }
    }

    private void SetupObstacles()
    {
        foreach(ObstacleController obstacle in obstacles)
        {
            SetupLine(obstacle, upperLineXOffset, obstacle.heightOffset, obstacle.width, obstacle.position);
            obstacle.position -= (width/scale * Time.deltaTime);
        }

        if (obstacles.TryPeek(out ObstacleController top))
        {
            if (top.position < -width - top.width)
            {
                Destroy(top.gameObject);
                obstacles.Dequeue();
            }
        }
    }

    private void SetupLines()
    {
        upperLinePoints = SetupLine(upperLine, upperLineXOffset, upperLineHeightOffset, width);
        lowerLinePoints = SetupLine(lowerLine, lowerLineXOffset, lowerLineHeightOffset, width);
        upperLineXOffset += (speed * Time.deltaTime);
        lowerLineXOffset += (speed * Time.deltaTime);
    }

    private List<Vector2> SetupLine(LineController line, float xOffset, float heightOffset, float length, float position = 0)
    {
        List<Vector2> points = new();
        for (float i = position - length; i <= position + length + stepSize / 2; i += stepSize)
        {
            float x = (((float)i / (float)width) * scale) + xOffset;
            float height = Mathf.PerlinNoise(x, 0);
            points.Add(new Vector2(i, heightScale * height + heightOffset));
        }
        line.SetupLine(points.ToArray());
        return points;
    }

    void Update()
    {
        SetupLines();
        SetupObstacles();
    }
}
