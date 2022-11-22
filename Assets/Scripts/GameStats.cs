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
        PlayerDamage = 15; // default
        MaxPlayerHealth = 3;
        MaxPlayerMana = 5;

        Score = 0;
        HighScore = 0;
        ScoreModifier = 1;

        Difficulty = 1;
        HighestDifficultyReached = 0;

        gameUI = FindObjectOfType<GameUI>();
    }

    private void Update()
    {

    }
}
