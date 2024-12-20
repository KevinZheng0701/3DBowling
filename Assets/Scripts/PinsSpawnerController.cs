using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinsSpawnerController : MonoBehaviour
{
    public GameObject pinPrefab;  // Prefab for the pins
    public Transform[] pinsSpawnLocation; // Locations where the pins will spawn
    private int fallenPins; // Counter for fallen pins
    private List<GameObject> pins = new List<GameObject>(); // List to store spawned pins
    public GameManager gameManager;  // Reference to the GameManager
    public HoldAndShoot holdAndShootScript;  // Reference to the HoldAndShoot script

    // Initialize and spawn the pins at the beginning
    void Start()
    {
        SpawnPins();
    }

    // Spawn the pins at predefined locations
    private void SpawnPins()
    {
        foreach (Transform spawnLocation in pinsSpawnLocation)
        {
            GameObject pin = Instantiate(pinPrefab, spawnLocation.position, pinPrefab.transform.rotation);
            pin.transform.SetParent(transform); // Set the parent of the pin to this GameObject
            pins.Add(pin); // Add pin to the list
        }
        fallenPins = 0; // Reset fallen pins counter
        holdAndShootScript.AllowThrow(); // Allow the player to throw the ball
    }

    // Reset all pins back to their original positions
    public void ResetPins()
    {
        for (int i = 0; i < pins.Count; i++)
        {
            GameObject pin = pins[i];
            pin.transform.position = pinsSpawnLocation[i].position;
            pin.transform.rotation = pinPrefab.transform.rotation;
            pin.SetActive(true);
        }
        fallenPins = 0;
        holdAndShootScript.AllowThrow();
    }

    // Handle the reporting of fallen pins and check if all pins are down
    public void ReportPinStatus()
    {
        fallenPins++; // Increment fallen pins counter
        // If all pins are down, proceed to next round
        if (fallenPins == pins.Count)
        {
            bool isNextRound = gameManager.HaveNextRound(); // Get the next round status
            if (isNextRound)
            {
                Invoke("ResetPins", 3f);
            }
        }
    }
}
