using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Library for scene management

public class SceneChanger : MonoBehaviour
{
    // Function to move to new scene
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID); // Load the new scene
    }
}