using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // Update is called once per frame
    void Update()
    {

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
        holdAndShootScript.canThrow = true;
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
        holdAndShootScript.canThrow = true;
    }

    public void ReportPinStatus()
    {
        fallenPins += 1;
        if (fallenPins == pins.Count)
        {
            bool isNextRound = gameManager.HaveNextRound();
            if (isNextRound)
            {
                Invoke("ResetPins", 3f); // Reset after 3 seconds
            }
        }
    }
}
