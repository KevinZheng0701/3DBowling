using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Rigidbody ballRb; // Rigid body of the ball

    // Start is called before the first frame update
    void Start()
    {
        GameObject invisibleWall = GameObject.FindGameObjectWithTag("Invisible");
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), invisibleWall.GetComponent<Collider>());
    }

    // Function to apply force to the ball
    public void ApplyForceToBall(float force)
    {
        ballRb.useGravity = true;
        ballRb.AddForce(transform.parent.transform.forward * force / ballRb.mass);
    }
}