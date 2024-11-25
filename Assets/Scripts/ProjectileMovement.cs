using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour 
{
    public Rigidbody projectileRb; // Rigid body of the projectile
    private HoldAndShoot holdAndShootScript; // Reference to the hold and shoot script
    private float projectileLife = 10f; 

    // Start is called before the first frame update
    void Start()
    {
        holdAndShootScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HoldAndShoot>();
        Invoke("SelfDestroy", projectileLife);
    }

    // Check for collision
    private void OnCollisionEnter(Collision collision)
    {
        holdAndShootScript.HandleProjectileCollisionBeforeShooting(gameObject);
    }

    public void ApplyForceToProjectile(float force)
    {
        projectileRb.useGravity = true;
        projectileRb.AddForce(transform.forward * force) ;
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}