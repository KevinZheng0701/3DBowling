using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour 
{
    public Rigidbody projectileRb; // Rigid body of the projectile

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Check for collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") // Collide with enemy objects
        {
            collision.gameObject.SetActive(false); // Destroy the damage object
        }
        Destroy(gameObject); // Destroy the projectile
    }

    public void ApplyForceToProjectile(float force)
    {
        projectileRb.AddForce(Vector3.forward * force);
        projectileRb.useGravity = true;
    }
}