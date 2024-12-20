using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts;        // Text elements for the round scores
    public TextMeshProUGUI totalScoreText;      // Text element for total score
    public TextMeshProUGUI gameOverScoreText;   // Text element for game over score
    public GameObject gameOverScreen;           // Game over screen UI

    // Updates the score for the current round
    public void UpdateScore(int score, int round)
    {
        TextMeshProUGUI scoreText = scoreTexts[round];
        scoreText.text = score.ToString();
    }

    // Updates the total score
    public void UpdateTotalScore(int score)
    {
        totalScoreText.text = "Total: " + score.ToString();
    }

    // Displays the game over screen with the total score
    public void ShowGameOverScreen(int totalScore)
    {
        gameOverScoreText.text = "Your total score: " + totalScore.ToString();
        gameOverScreen.SetActive(true);
    }
}
