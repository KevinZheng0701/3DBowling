using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPinController : MonoBehaviour
{
    public float lifespan = 5f; // Time until pin is removed after falling
    public Rigidbody pinRb; // Rigidbody of the pin
    private bool haveFallen; // Tracks if the pin has fallen
    private PinsSpawnerController pinsSpawnerController; // Reference to the PinsSpawnerController
    public AudioSource[] soundEffects; // Sound effects (first for pin collision, second for ball collision)

    // Initialize necessary components and variables
    void Start()
    {
        pinsSpawnerController = transform.parent.GetComponent<PinsSpawnerController>();
    }

    // Reset pin state when enabled
    void OnEnable()
    {
        pinRb.velocity = Vector3.zero;
        pinRb.angularVelocity = Vector3.zero;
        lifespan = 5f;
        haveFallen = false;
    }

    // Update is called once per frame to check pin status
    void Update()
    {
        if (haveFallen)
        {
            // Countdown lifespan and deactivate the pin if it expires
            lifespan -= Time.deltaTime;
            if (lifespan < 0)
            {
                pinsSpawnerController.ReportPinStatus(); // Report pin status to the spawner
                gameObject.SetActive(false); // Deactivate the pin
            }
        }
        else
        {
            // Check if the pin has fallen by examining its rotation
            float xRotation = transform.rotation.eulerAngles.x;
            float zRotation = transform.rotation.eulerAngles.z;
            // If pin is near one of the fallen states, set it as fallen
            if (Mathf.Abs(xRotation) < 0.1f || Mathf.Abs(xRotation - 180f) < 0.1f || Mathf.Abs(zRotation - 90f) < 0.1f || Mathf.Abs(zRotation - 270f) < 0.1f)
            {
                haveFallen = true;
            }
        }
    }

    // Handle collision with other objects
    private void OnCollisionEnter(Collision collision)
    {
        if (!haveFallen && collision.gameObject.CompareTag("Pin"))
        {
            soundEffects[0].Play(); // Play pin-to-pin sound
        }
        else if (collision.gameObject.CompareTag("Ball"))
        {
            soundEffects[1].Play(); // Play ball-to-pin sound
        }
    }
}
