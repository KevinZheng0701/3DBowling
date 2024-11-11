using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum mode
    {
        RED,
        GREEN,
        BLUE
    }
    public mode myMode;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (myMode)
        {
            case mode.RED:
                material.color = Color.red;
                break;
            case mode.GREEN:
                material.color = Color.green;
                break;
            case mode.BLUE:
                material.color = Color.blue;
                break;
            default:
                material.color = Color.white;
                break;
        }
    }
}
