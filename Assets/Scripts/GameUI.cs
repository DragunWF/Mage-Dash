using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI difficultyText;
    private Slider healthSlider;
    private Slider manaSlider;

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
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        manaSlider = GameObject.Find("ManaSlider").GetComponent<Slider>();
    }

    private void Start()
    {
        scoreText.text = "Score: 0";
        difficultyText.text = "Difficulty: 1";
    }
}
