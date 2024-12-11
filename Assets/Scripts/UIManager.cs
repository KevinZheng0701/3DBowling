using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts;
    public TextMeshProUGUI totalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score, int round)
    {
        TextMeshProUGUI scoreText = scoreTexts[round];
        scoreText.text = score.ToString();
    }

    public void UpdateTotalScore(int score)
    {
        totalScoreText.text = "Total: " + score.ToString();
    }
}
