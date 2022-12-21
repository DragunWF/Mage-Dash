using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class GameUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI difficultyText;
    private TextMeshProUGUI powerupText;

    private Slider healthSlider;
    private Slider manaSlider;

    private DifficultyScaling difficulty;

    private Dictionary<string, string> potionDescriptions = new Dictionary<string, string>();
    private List<string> activePowerups = new List<string>();

    public void ModifyPowerups(string type, bool addItem)
    {
        powerupText.text = "Active Powerups:\n";

        if (addItem && !activePowerups.Contains(type))
        {
            activePowerups.Add(type);
        }
        else
        {
            activePowerups.Remove(type);
        }

        if (activePowerups.Count > 0)
        {
            foreach (string powerup in activePowerups)
            {
                powerupText.text += string.Format("- {0}\n", potionDescriptions[powerup]);
            }
        }
        else
        {
            powerupText.text += "- None\n";
        }
    }

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
        string formattedScore = Utils.FormatNumber(newScore);
        scoreText.text = string.Format("Score: {0}", formattedScore);
    }

    public void UpdateDifficulty(int newDifficulty)
    {
        string levelDisplay = newDifficulty >= difficulty.GetMaxDifficultyLevel() ?
                              "Max" : newDifficulty.ToString();
        difficultyText.text = string.Format("Difficulty: {0}", levelDisplay);
    }

    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        difficultyText = GameObject.Find("DifficultyText").GetComponent<TextMeshProUGUI>();
        powerupText = GameObject.Find("PowerupText").GetComponent<TextMeshProUGUI>();

        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        manaSlider = GameObject.Find("ManaSlider").GetComponent<Slider>();

        difficulty = FindObjectOfType<DifficultyScaling>();

        potionDescriptions.Add("mana", "Increased Mana Regen & Capacity");
        potionDescriptions.Add("score", "Double Score");
        potionDescriptions.Add("damage", "Double Damage");
    }

    private void Start()
    {
        scoreText.text = "Score: 0";
        difficultyText.text = "Difficulty: 1";
        powerupText.text = "Active Powerups:\n- None";
    }
}
