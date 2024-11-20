using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour 
{
    public Rigidbody projectileRb; // Rigid body of the projectile
    private HoldAndShoot holdAndShootScript; // Reference to the hold and shoot script

    // Start is called before the first frame update
    void Start()
    {
        holdAndShootScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HoldAndShoot>();
    }

    // Check for collision
    private void OnCollisionEnter(Collision collision)
    {
        holdAndShootScript.HandleProjectileCollisionBeforeShooting();
        if (collision.gameObject.tag == "Enemy") // Collide with enemy objects
        {
            collision.gameObject.SetActive(false); // Destroy the damage object
        }
        Destroy(gameObject); // Destroy the projectile
    }

    public void ApplyForceToProjectile(float force)
    {
        projectileRb.useGravity = true;
        projectileRb.AddForce(transform.forward * force) ;
    }
}