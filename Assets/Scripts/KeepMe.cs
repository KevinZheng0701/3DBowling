using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMe : MonoBehaviour
{
    public KeepMe Instance;

    // Called when the scripts are loading
    private void Awake()
    {
        if (Instance == null) // Instance hasn't been set
        {
            Instance = this; // Set the instance to the game object it is attached to
            DontDestroyOnLoad(gameObject); // Prevent the game object from being destroyed when loading
        }
        else // Instance is already set
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }
}