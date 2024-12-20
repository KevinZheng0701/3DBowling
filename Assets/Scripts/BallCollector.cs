using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollector : MonoBehaviour
{
    private List<GameObject> balls = new List<GameObject>(); // List to hold the collected balls

    // Returns the current number of balls in the collection
    public int GetNumberOfBalls()
    {
        return balls.Count;
    }

    // Adds a new ball to the collection
    public void AddBall(GameObject ball)
    {
        balls.Add(ball);
    }

    // Destroys all collected balls and clears the list
    public void DestroyAllBalls()
    {
        // Destroy all balls in the collection
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        // Clear the list after destruction
        balls.Clear();
        // Ensure all remaining balls are also destroyed
        GameObject[] remainingBalls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in remainingBalls)
        {
            Destroy(ball);
        }
    }
}
