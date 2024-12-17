using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldAndShoot : MonoBehaviour
{
    public enum BallStatus
    {
        Empty,
        Charging,
        Full
    }
    public BallStatus ballStatus; // Status of the ball
    public float chargeValue; // Value of the charge
    public float chargeRate; // Change in the charge value
    private float currentChargeValue; // Current charge value
    private float currentChargeRate; // Current change in the charge value
    public float chargeThreshold; // Maximum charge value
    public GameObject projectilePrefab; // Projectile prefab
    public Transform projectileSpawnPos; // The spawn position transform of the projectile
    public float throwForce; // Force of throwing
    private GameObject currentProjectile; // The current projectile the player is holding and charging
    private BallMovement ballMovementScript; // Reference to the ball movement script
    private Vector3 projectileScale; // Scale of the projectile
    public bool canThrow; // Whether the player can start shooting
    public int ballsThrown; // The number of times the player throw the ball
    public ChargeBar chargeBarScript; // Reference to the charge ball script

    // Start is called before the first frame update
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

    // Function to handle charging of the projectile
    private void ChargeProjectile()
    {
        if (Input.GetMouseButton(0) && ballStatus != BallStatus.Full)
        {
            currentChargeValue += currentChargeRate * Time.deltaTime;
            currentChargeRate -= 0.3f * Time.deltaTime;
            if (ballStatus == BallStatus.Empty)
            {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, projectilePrefab.transform.rotation);
                currentProjectile = projectile;
                projectile.transform.parent = transform;
                ballMovementScript = projectile.GetComponent<BallMovement>();
                ballStatus = BallStatus.Charging;
                chargeBarScript.ShowChargeBar();
            }
            if (currentChargeValue >= chargeThreshold)
            {
                ballStatus = BallStatus.Full;
            }
            UpdateProjectileScale();
            chargeBarScript.UpdateChargeValue(currentChargeValue);
        }
    }

    // Function to shoot the projectile
    private void ShootProjectile()
    {
        if (Input.GetMouseButtonUp(0) && ballStatus != BallStatus.Empty)
        {
            ballMovementScript.ApplyForceToBall(throwForce * currentChargeValue);
            ballsThrown += 1;
            ResetProjectile();
        }
    }

    // Function to update the scale
    private void UpdateProjectileScale()
    {
        projectileScale.x = currentChargeValue;
        projectileScale.y = currentChargeValue;
        projectileScale.z = currentChargeValue;
        if (currentProjectile)
        {
            currentProjectile.transform.localScale = projectileScale;
            ballMovementScript.ballRb.mass += currentChargeRate * Time.deltaTime * 0.25f;
        }
    }

    // Function to update the position
    private void UpdateProjectilePos()
    {
        if (currentProjectile)
        {
            currentProjectile.transform.position = projectileSpawnPos.position;
        }
    }

    // Function to reset everything regarding the projectile and the charge value and rate
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
}
