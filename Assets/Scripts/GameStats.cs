using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public int Score { get; private set; }
    public int Difficulty { get; private set; }
    public int HighScore { get; private set; }
    public int HighestDifficultyReached { get; private set; }

    public float ScoreModifier { get; private set; }

    private GameUI gameUI;

    public void IncreaseScore(int amount)
    {
        Score += amount;
        gameUI.UpdateScore(Score);
    }
    
    public void IncreaseDifficulty()
    {
        Difficulty++;
        gameUI.UpdateDifficulty(Difficulty);
    }

    private void Awake()
    {
        gameUI = FindObjectOfType<GameUI>();
    }

    private void Update()
    {

    }
}
