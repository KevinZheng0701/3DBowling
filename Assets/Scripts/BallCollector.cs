using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollector : MonoBehaviour
{
    private List<GameObject> balls = new List<GameObject>();

    public int GetNumberOfBalls()
    {
        return balls.Count;
    }

    public void AddBall(GameObject ball)
    {
        balls.Add(ball);
    }

    public void DestroyAllBalls()
    {
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        balls.Clear();
        // Ensure all balls are destroyed
        GameObject[] remainingBalls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in remainingBalls)
        {
            Destroy(ball);
        }
    }
}
