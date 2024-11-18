using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; // Speed of the player
    public float jumpForce; // Force of the jump
    private bool isJumping; // Status to check whether player is in the air
    public Rigidbody rb; // Player's Rigidbody component

    void FixedUpdate()
    {
        MovePlayer();
        if (!isJumping)
        {
            Jump();
        }
    }

    // Calculate movement direction based on player input and rotation
    private Vector3 Direction()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // Create input direction vector and make it relative to player rotation
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical);
        return transform.TransformDirection(inputDirection);
    }

    // Apply movement to player
    private void MovePlayer()
    {
        transform.position += Direction() * speed * Time.fixedDeltaTime;
    }

    // Handle player jump when Jump key is pressed
    private void Jump()
    {
        if (Input.GetAxis("Jump") == 1) // Jump input detected
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply upward force
        }
    }

    // Reset jump flag when player touches the ground or other objects
    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;
    }
}