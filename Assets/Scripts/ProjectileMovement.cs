using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour 
{
    public Rigidbody projectileRb; // Rigid body of the projectile
    private float projectileLife = 10f; 

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ApplyForceToProjectile(float force)
    {
        projectileRb.useGravity = true;
        projectileRb.AddForce(transform.forward * force);
        Invoke("SelfDestroy", projectileLife);
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}