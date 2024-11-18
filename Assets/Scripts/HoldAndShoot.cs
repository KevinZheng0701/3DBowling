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
    public bool readyToShoot; // Whether the projectile can be shot
    public float chargeValue; // Value of the charge
    public float chargeRate; // Change in the charge value
    public float chargeThreshold; // Maximum charge value
    public GameObject projectilePrefab; // Projectile prefab
    public Vector3 projectileSpawnPos; // The spawn position transform of the projectile
    public Transform projectileTransform; // The transform of the projectile
    public ProjectileMovement projectileMovement; // Reference to the projectile movement script
    public Vector3 projectileScale; // Scale of the projectile
    public float projectileForce; // Force of projectile

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
        }
        else if (Input.GetMouseButton(0))
        {
            Debug.Log("HOLD");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Uncliked");
        }
        ShootProjectile();
        if (chargeValue >= chargeThreshold)
        {
            projectileStatus = ProjectileStatus.Full;
            readyToShoot = true;
        }
        else
        {
            ChargeProjectile();
        }
    }

    // Function to handle charging of the projectile
    private void ChargeProjectile()
    {
        if (Input.GetMouseButton(0))
        {
            chargeValue += chargeRate * Time.deltaTime;
            if (projectileStatus == ProjectileStatus.Empty)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position + projectileSpawnPos, Quaternion.identity);

                projectileTransform = projectile.transform;
                projectileMovement = projectile.GetComponent<ProjectileMovement>();
                projectileStatus = ProjectileStatus.Charging;
            }
            UpdateProjectileScale();
            readyToShoot = true;
         
        }
    }

    // Function to shoot the projectile
    private void ShootProjectile()
    {
        if (Input.GetMouseButtonDown(0) && readyToShoot)
        {
            projectileMovement.ApplyForceToProjectile(chargeValue * projectileForce);
            readyToShoot = false;
            chargeValue = 0;
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
}
