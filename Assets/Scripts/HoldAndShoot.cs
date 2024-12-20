using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldAndShoot : MonoBehaviour
{
    // Enum to track the status of the ball
    public enum BallStatus
    {
        Empty,
        Charging,
        Full
    }
    public BallStatus ballStatus; // Current status of the ball
    private bool canThrow; // Whether the player can throw the ball
    public float chargeValue; // Initial charge value
    public float chargeRate; // Rate at which the charge value changes
    private float currentChargeValue; // Current value of the charge
    private float currentChargeRate; // Current rate of charge change
    public float chargeThreshold; // Max charge value before the ball is considered full
    public float throwForce; // Force applied when the ball is thrown
    public Transform projectileSpawnPos; // Position where the ball spawns
    private GameObject currentProjectile; // Currently charged projectile
    public GameObject projectilePrefab; // The projectile prefab
    private Vector3 projectileScale; // Scale of the projectile
    public ChargeBar chargeBarScript; // Reference to the charge bar script
    private BallMovement ballMovementScript; // Reference to the ball movement script
    public BallCollector ballCollectorScript; // Reference to the ball collector script

    // Initialize variables
    void Start()
    {
        currentChargeRate = chargeRate;
        currentChargeValue = chargeValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (canThrow)
        {
            ChargeProjectile();
            ShootProjectile();
            UpdateProjectilePos();
        }
    }

    // Handle charging the projectile when holding the mouse button
    private void ChargeProjectile()
    {
        // If the mouse button is held and the ball is not fully charged
        if (Input.GetMouseButton(0) && ballStatus != BallStatus.Full)
        {
            // Increase charge value and decrease the charge rate over time
            currentChargeValue += currentChargeRate * Time.deltaTime;
            currentChargeRate -= 0.3f * Time.deltaTime;
            // Instantiate the projectile if it hasn't been created yet
            if (ballStatus == BallStatus.Empty)
            {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, projectilePrefab.transform.rotation);
                currentProjectile = projectile;
                projectile.transform.parent = transform;
                ballMovementScript = projectile.GetComponent<BallMovement>();
                ballStatus = BallStatus.Charging;
                chargeBarScript.ShowChargeBar();
            }
            // Set the ball to full charge if the threshold is reached
            if (currentChargeValue >= chargeThreshold)
            {
                ballStatus = BallStatus.Full;
            }
            // Update the projectile's scale and the charge bar
            UpdateProjectileScale();
            chargeBarScript.UpdateChargeValue(currentChargeValue);
        }
    }

    // Shoot the projectile when the mouse button is released
    private void ShootProjectile()
    {
        if (Input.GetMouseButtonUp(0) && ballStatus != BallStatus.Empty)
        {
            ballCollectorScript.AddBall(currentProjectile);
            ballMovementScript.ApplyForceToBall(throwForce * currentChargeValue);
            ResetProjectile();
        }
    }

    // Update the scale of the projectile based on the current charge value
    private void UpdateProjectileScale()
    {
        projectileScale.x = currentChargeValue;
        projectileScale.y = currentChargeValue;
        projectileScale.z = currentChargeValue;
        if (currentProjectile)
        {
            currentProjectile.transform.localScale = projectileScale;
            ballMovementScript.ballRb.mass += currentChargeRate * Time.deltaTime * 0.2f; // Adjust mass based on charge rate
        }
    }

    // Update the projectile's position to match the spawn position
    private void UpdateProjectilePos()
    {
        if (currentProjectile)
        {
            currentProjectile.transform.position = projectileSpawnPos.position;
        }
    }

    // Reset everything related to the projectile and charge state
    public void ResetProjectile()
    {
        ballStatus = BallStatus.Empty;
        currentChargeValue = chargeValue;
        currentChargeRate = chargeRate;
        ballMovementScript = null;
        if (currentProjectile)
        {
            currentProjectile.transform.parent = null;
        }
        currentProjectile = null;
        chargeBarScript.ResetValue();
        chargeBarScript.HideChargeBar();
    }

    // Stop the ability to throw the ball
    public void StopThrow()
    {
        canThrow = false;
    }

    // Allow the player to throw the ball
    public void AllowThrow()
    {
        canThrow = true;
    }
}
