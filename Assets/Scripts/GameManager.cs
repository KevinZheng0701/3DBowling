using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int round;
    public float score;
    public float totalScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HaveNextRound()
    {
        if (round < 10)
        {
            round++;
            totalScore += score;
            score = 0;
            return true;
        }
        return false;
    }

}
