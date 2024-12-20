using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int round; // Current round of the game
    private int score; // Score for the current round
    private int totalScore; // Total score accumulated throughout the game
    public HoldAndShoot holdAndShootScript; // Reference to the HoldAndShoot script
    public BallCollector ballCollectorScript; // Reference to the BallCollector script
    public UIManager uiManager; // Reference to the UIManager script

    // Checks if the game should proceed to the next round
    public bool HaveNextRound()
    {
        // Reset throwing mechanics for the next round
        holdAndShootScript.StopThrow();
        holdAndShootScript.ResetProjectile();
        // Calculate score based on the number of balls thrown
        int ballsThrown = ballCollectorScript.GetNumberOfBalls();
        score += CalculateRoundScore(ballsThrown);
        ballCollectorScript.DestroyAllBalls(); // Destroy all balls after calculating the score
        // Update UI with the current and total score
        totalScore += score;
        uiManager.UpdateScore(score, round);
        uiManager.UpdateTotalScore(totalScore);
        // Prepare for the next round
        round++;
        score = 0;
        // Check if the game has more rounds left
        if (round < 10)
        {
            return true;
        }
        // Show game over screen if all rounds are finished
        uiManager.ShowGameOverScreen(totalScore);
        // Unlock the cursor for the game over screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        return false;
    }

    // Calculates the score for the current round based on the number of balls thrown
    private int CalculateRoundScore(int ballsThrown)
    {
        int roundScore = 0;
        // Add 6 points for a strike (1 ball thrown)
        if (ballsThrown == 1)
        {
            roundScore += 6;
        }
        // Add 2 points for a spare (2 balls thrown)
        else if (ballsThrown == 2)
        {
            roundScore += 2;
        }
        // Add remaining points based on how many balls were thrown
        roundScore += Mathf.Max(0, 10 - ballsThrown);
        return roundScore;
    }
}
