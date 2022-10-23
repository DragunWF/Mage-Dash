using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI difficultyText;

    public void UpdateScore(int newScore)
    {
        scoreText.text = string.Format("Score: {0}", newScore);
    }

    public void UpdateDifficulty(int newDifficulty)
    {
        difficultyText.text = string.Format("Difficulty: {0}", newDifficulty);
    }

    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        difficultyText = GameObject.Find("DifficultyText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        scoreText.text = "Score: 0";
        difficultyText.text = "Difficulty: 1";
    }
}
