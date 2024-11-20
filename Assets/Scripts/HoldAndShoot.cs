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
    private Transform projectileTransform; // The transform of the projectile
    private ProjectileMovement projectileMovement; // Reference to the projectile movement script
    private Vector3 projectileScale; // Scale of the projectile

    // Start is called before the first frame update
    void Start()
    {   

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.black);
        ShootProjectile();
        ChargeProjectile();
    }

    // Function to handle charging of the projectile
    private void ChargeProjectile()
    {
        if (Input.GetMouseButton(0) && projectileStatus != ProjectileStatus.Full)
        {
            chargeValue += chargeRate * Time.deltaTime;
            if (projectileStatus == ProjectileStatus.Empty)
            {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, transform.rotation);
                projectile.transform.parent = transform;
                projectileTransform = projectile.transform;
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
        if (Input.GetMouseButtonUp(0))
        {
            projectileMovement.ApplyForceToProjectile(chargeValue * projectileForce);
            chargeValue = 0.1f;
            projectileStatus = ProjectileStatus.Empty;
        }
    }

    // Function to update the scale
    private void UpdateProjectileScale()
    {
        projectileScale.x = chargeValue;
        projectileScale.y = chargeValue;
        projectileScale.z = chargeValue;
        projectileTransform.localScale = projectileScale;
    }
    
    // Handles the case when the projectile is still charging and collided with a game object
    public void HandleProjectileCollisionBeforeShooting()
    {
        projectileStatus = ProjectileStatus.Empty;
        chargeValue = 0.1f;
        projectileTransform = null;
        projectileMovement = null;
    }
}
