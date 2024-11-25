using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldAndShoot : MonoBehaviour
{
    public enum ProjectileStatus
    {
        Empty,
        Charging,
        Full
    }
    public ProjectileStatus projectileStatus; // Status of the projectile
    public float chargeValue; // Value of the charge
    public float chargeRate; // Change in the charge value
    public float chargeThreshold; // Maximum charge value
    public GameObject projectilePrefab; // Projectile prefab
    public Transform projectileSpawnPos; // The spawn position transform of the projectile
    public float projectileForce; // Force of projectile
    private GameObject currentProjectile; // The current projectile the player is holding and charging
    private ProjectileMovement projectileMovement; // Reference to the projectile movement script
    private Vector3 projectileScale; // Scale of the projectile

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.black);
        ChargeProjectile();
        ShootProjectile();
        UpdateProjectilePos();
    }

    // Function to handle charging of the projectile
    private void ChargeProjectile()
    {
        if (Input.GetMouseButton(0) && projectileStatus != ProjectileStatus.Full)
        {
            chargeValue += chargeRate * Time.deltaTime;
            chargeRate -= 0.3f * Time.deltaTime;
            if (projectileStatus == ProjectileStatus.Empty)
            {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, transform.rotation);
                currentProjectile = projectile;
                projectile.transform.parent = transform;
                projectileMovement = projectile.GetComponent<ProjectileMovement>();
                projectileStatus = ProjectileStatus.Charging;
            }
            if (chargeValue >= chargeThreshold)
            {
                projectileStatus = ProjectileStatus.Full;
            }
            UpdateProjectileScale();
        }
    }

    // Function to shoot the projectile
    private void ShootProjectile()
    {
        if (Input.GetMouseButtonUp(0) && projectileStatus != ProjectileStatus.Empty)
        {
            projectileMovement.ApplyForceToProjectile(chargeValue * projectileForce);
            ResetProjectile();
        }
    }

    // Function to update the scale
    private void UpdateProjectileScale()
    {
        projectileScale.x = chargeValue;
        projectileScale.y = chargeValue;
        projectileScale.z = chargeValue;
        if (currentProjectile)
        {
            currentProjectile.transform.localScale = projectileScale;
        }
    }

    // Function to update the position
    private void UpdateProjectilePos()
    {
        Debug.Log(currentProjectile);
        if (currentProjectile)
        {
            currentProjectile.transform.position = projectileSpawnPos.position;
        }
    }

    // Function to reset everything regarding the projectile and the charge value and rate
    private void ResetProjectile()
    {
        projectileStatus = ProjectileStatus.Empty;
        chargeValue = 0.1f;
        chargeRate = 0.75f;
        projectileMovement = null;
        currentProjectile.transform.parent = null;
        currentProjectile = null;
    }
    
    // Handles the case when the projectile is still charging and collided with a game object
    public void HandleProjectileCollisionBeforeShooting(GameObject projectile)
    {
        if (currentProjectile == projectile)
        {
            ResetProjectile();
        }
    }
}
