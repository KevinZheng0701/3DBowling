using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int round;
    public int score;
    public int totalScore;
    public HoldAndShoot holdAndShootScript;
    public UIManager uiManager;

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
        holdAndShootScript.canThrow = false;
        // Destroy all previous balls
        holdAndShootScript.ResetProjectile();
        GameObject[] remainingBalls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in remainingBalls)
        {
            Destroy(ball);
        }
        // Calculate the score
        int shots = holdAndShootScript.ballsThrown;
        switch(shots)
        {
            case 1:
                score = 10;
                break;
            case 2:
                score = 8;
                break;
            case 3:
                score = 5;
                break;
            case 4:
                score = 1;
                break;
            default:
                score = 0;
                break;
        }
        uiManager.UpdateScore(score, round);
        totalScore += score;
        uiManager.UpdateTotalScore(totalScore);
        round++;
        score = 0;
        if (round < 10)
        {
            return true;
        }
        return false;
    }

}
