using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Rigidbody ballRb; // The rigid body of the ball
    public AudioSource rollingSound; // The audio source for the rolling sound

    // Start is called before the first frame update
    void Start()
    {
        // Ignore collision with the invisible wall
        GameObject invisibleWall = GameObject.FindGameObjectWithTag("Invisible");
        if (invisibleWall)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), invisibleWall.GetComponent<Collider>());
        }
    }

    // Applies force to the ball and plays the rolling sound
    public void ApplyForceToBall(float force)
    {
        // Enable gravity and apply force in the forward direction of the parent
        ballRb.useGravity = true;
        ballRb.AddForce(transform.parent.transform.forward * force / ballRb.mass);
        rollingSound.Play(); // Play the rolling sound
    }
}
