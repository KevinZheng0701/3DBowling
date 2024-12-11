using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour 
{
    public Rigidbody projectileRb; // Rigid body of the projectile

    // Start is called before the first frame update
    void Start()
    {
        GameObject invisibleWall = GameObject.FindGameObjectWithTag("Invisible");
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), invisibleWall.GetComponent<Collider>());
    }

    public void ApplyForceToProjectile(float force)
    {
        projectileRb.useGravity = true;
        projectileRb.AddForce(transform.forward * force / projectileRb.mass);
    }

}