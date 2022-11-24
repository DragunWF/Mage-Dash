using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public sealed class RetryMenuUI : MonoBehaviour
{
    private GameManager gameManager;
    private GameStats gameStats;

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreText;
    private GameObject newHighScoreText;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameStats = FindObjectOfType<GameStats>();

        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        highScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        newHighScoreText = GameObject.Find("NewHighScoreText");
    }

    private void Start()
    {
        scoreText.text = string.Format("Score: {0}", gameStats.Score);
        highScoreText.text = string.Format("High Score: {0}", gameStats.HighScore);
        newHighScoreText.SetActive(gameStats.NewHighScore);
    }
}
