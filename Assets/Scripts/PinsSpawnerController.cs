using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinsSpawnerController : MonoBehaviour
{
    public GameObject pinPrefab;
    public Transform[] pinsSpawnLocation;
    public GameManager gameManager;
    private List<GameObject> pins = new List<GameObject>();
    private int fallenPins;
    public HoldAndShoot holdAndShootScript;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPins();
    }

    private void SpawnPins()
    {
        for (int i = 0; i < pinsSpawnLocation.Length; i++)
        {
            GameObject pin = Instantiate(pinPrefab, pinsSpawnLocation[i].position, pinPrefab.transform.rotation);
            pin.transform.SetParent(transform);
            pins.Add(pin);
        }
        fallenPins = 0;
        holdAndShootScript.AllowThrow();
    }

    public void ResetPins()
    {
        for (int i = 0; i < pinsSpawnLocation.Length; i++)
        {
            GameObject pin = pins[i];
            pin.transform.position = pinsSpawnLocation[i].position;
            pin.transform.rotation = pinPrefab.transform.rotation;
            fallenPins = 0;
            pin.SetActive(true);
        }
        holdAndShootScript.AllowThrow();
    }

    public void ReportPinStatus()
    {
        fallenPins += 1;
        Debug.Log(fallenPins);
        if (fallenPins == pins.Count)
        {
            bool isNextRound = gameManager.HaveNextRound();
            if (isNextRound)
            {
                Invoke("ResetPins", 3f);
            }
        }
    }
}
