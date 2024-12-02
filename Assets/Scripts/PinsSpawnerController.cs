using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PinsSpawnerController : MonoBehaviour
{
    public GameObject pinPrefab;
    public Transform[] pinsSpawnLocation;
    public List<GameObject> pins = new List<GameObject>();

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
            GameObject pin = Instantiate(pinPrefab, pinsSpawnLocation[i].position, Quaternion.identity);
            pins.Append(pin);
        }
    }

    public void ResetPins()
    {
        for (int i = 0; i < pinsSpawnLocation.Length; i++)
        {
            GameObject pin = pins[i];
            pin.transform.position = pinsSpawnLocation[i].position;
            pin.SetActive(true);
        }
    }
}
