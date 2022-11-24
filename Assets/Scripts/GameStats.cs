using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public int Score { get; private set; }
    public int Difficulty { get; private set; }

    public int HighScore { get; private set; }
    public int HighestDifficultyReached { get; private set; }

    public int PlayerDamage { get; private set; }
    public int MaxPlayerHealth { get; private set; }
    public int MaxPlayerMana { get; private set; }

    public int HealthLevel { get; private set; }
    public int DamageLevel { get; private set; }
    public int ManaRegenLevel { get; private set; }
    public int AgilityLevel { get; private set; }

    public float ScoreModifier { get; private set; }

    private float scoreMultiplier = 1f;
    private GameUI gameUI;

    public void IncreaseScore(float amount)
    {
        Score += (int)Mathf.Round(amount * scoreMultiplier);
        gameUI.UpdateScore(Score);
    }

    public void IncreaseDifficulty()
    {
        Difficulty++;
        gameUI.UpdateDifficulty(Difficulty);
    }

    private void Awake()
    {
        PlayerDamage = 15; // default values
        MaxPlayerHealth = 3;
        MaxPlayerMana = 5;

        DamageLevel = 1;
        ManaRegenLevel = 1;
        AgilityLevel = 1; // increase score multiplier

        Score = 0;
        HighScore = 0;
        ScoreModifier = 1;

        Difficulty = 1;
        HighestDifficultyReached = 0;

        gameUI = FindObjectOfType<GameUI>();
    }

    private float ComputeScoreMultiplier()
    {
        const float baseMultiplier = 1f;
        const float levelIncrease = 0.25f;
        return baseMultiplier + levelIncrease * AgilityLevel;
    }

    private int ComputeHealth()
    {
        const int baseHealth = 3;
        return HealthLevel == 1 ? baseHealth : baseHealth + HealthLevel - 1;
    }

    private float ComputeManaRegen()
    {
        const float manaLimit = 0.25f;
        const float decrementor = 0.3f;
        float manaRegenTime = 3.5f;
        manaRegenTime -= ManaRegenLevel * decrementor;

        if (manaRegenTime <= manaLimit)
        {
            manaRegenTime = manaLimit;
        }

        return manaRegenTime;
    }
}
