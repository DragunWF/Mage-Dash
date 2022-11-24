using System;
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

    public void UpdateHealthBar(int newValue, bool updateMaxHealth = false)
    {
        if (updateMaxHealth)
        {
            healthSlider.maxValue = newValue;
        }
        healthSlider.value = newValue;
    }

    public void UpdateManaBar(int newValue, bool updateMaxMana = false)
    {
        if (updateMaxMana)
        {
            manaSlider.maxValue = newValue;
        }
        manaSlider.value = newValue;
    }

    public void UpdateScore(int newScore)
    {
        string formattedScore = FormatScore(newScore);
        scoreText.text = string.Format("Score: {0}", formattedScore);
    }

    public void UpdateDifficulty(int newDifficulty, bool maxDifficulty)
    {
        string levelDisplay = maxDifficulty ? "Max" : newDifficulty.ToString();
        difficultyText.text = string.Format("Difficulty: {0}", levelDisplay);
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

    private string FormatScore(int points)
    {
        if (points < 1000)
        {
            return points.ToString();
        }

        string formatted = "", str = points.ToString();
        for (int i = 1, n = str.Length; i <= n; i++)
        {
            formatted += str[str.Length - i];
            if (i + 1 <= n && i % 3 == 0)
            {
                formatted += ",";
            }
        }

        char[] charArr = formatted.ToCharArray();
        Array.Reverse(charArr);

        return new string(charArr);
    }
}
