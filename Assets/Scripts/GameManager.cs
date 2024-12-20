using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int round;
    private int score;
    private int totalScore;
    public HoldAndShoot holdAndShootScript;
    public BallCollector ballCollectorScript;
    public UIManager uiManager;

    public bool HaveNextRound()
    {
        holdAndShootScript.StopThrow();
        holdAndShootScript.ResetProjectile();
        // Calculate the score
        int ballsThrown = ballCollectorScript.GetNumberOfBalls();
        // Add 6 extra points for a strike
        if (ballsThrown == 1)
        {
            score += 6;
        }
        else if (ballsThrown == 2) // Add 2 points for a spare
        {
            score += 2;
        }
        score += Mathf.Max(0, 10 - ballsThrown);
        ballCollectorScript.DestroyAllBalls();
        totalScore += score;
        uiManager.UpdateScore(score, round);
        uiManager.UpdateTotalScore(totalScore);
        round++;
        score = 0;
        Debug.Log(round);
        if (round < 10)
        {
            return true;
        }
        return false;
    }

}
