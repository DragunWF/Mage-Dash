using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameStats : MonoBehaviour
{
    public int Score { get; private set; }
    public int Difficulty { get; private set; }
    public int Coins { get; private set; }
    public int SessionCoinsCollected { get; private set; }

    public int HighScore { get; private set; }
    public int HighestDifficultyReached { get; private set; }
    public bool NewHighScore { get; private set; }

    public int MaxPlayerHealth { get; set; } // Updated at Start() of Player.cs
    public int MaxPlayerMana { get; set; } // Updated at Start() of Player.cs

    public int HealthLevel { get; private set; }
    public int SpellLevel { get; private set; }
    public int ManaLevel { get; private set; }

    public float ScoreModifier { get; private set; }
    public bool CodeUsed { get; private set; }

    private float scoreMultiplier = 1f;
    private const int maxLevel = 10;
    private const int healthMaxLevel = 5; // Specific to health stat for balancing

    private GameUI gameUI;
    private static GameStats instance;

    #region Single Line Methods

    public int GetMaxLevel() => maxLevel;
    public int GetHealthMaxLevel() => healthMaxLevel;
    public void SubtractCoins(int amount) => Coins -= amount;

    #endregion

    #region Compute Methods

    public float ComputeManaRegen()
    {
        const float manaLimit = 0.25f, baseRegenTime = 3.5f;
        const float decrementor = 0.3f;
        float manaRegenTime = baseRegenTime; // base value

        if (ManaLevel > 1)
        {
            float decreasedAmount = (ManaLevel - 1) * decrementor;
            manaRegenTime = baseRegenTime - decreasedAmount;
            if (manaRegenTime <= manaLimit)
            {
                manaRegenTime = manaLimit;
            }
        }

        return manaRegenTime;
    }

    public int ComputeManaCapacity()
    {
        const int baseManaCapacity = 5;
        if (ManaLevel > 1)
        {
            const int manaChange = 2;
            return (ManaLevel - 1) / manaChange + baseManaCapacity;
        }
        return baseManaCapacity;
    }

    public int ComputeHealth()
    {
        const int baseHealth = 2;
        return HealthLevel > 1 ? HealthLevel - 1 + baseHealth : baseHealth;
    }

    public int ComputeDamage()
    {
        const int baseDamage = 15;
        int damage = baseDamage;

        if (SpellLevel > 1)
        {
            damage += (SpellLevel - 1) * 4;
        }

        return damage;
    }

    #endregion

    public void UpgradeStat(string type, int price)
    {
        SubtractCoins(price);

        switch (type)
        {
            case "health":
                HealthLevel++;
                break;
            case "mana":
                ManaLevel++;
                break;
            case "spell":
                SpellLevel++;
                break;
        }
    }

    public void SaveScore()
    {
        if (Score > HighScore)
        {
            HighScore = Score;
            NewHighScore = true;
        }
    }

    public void OnGameReset()
    {
        Score = 0;
        SessionCoinsCollected = 0;
        NewHighScore = false;
        Difficulty = 0;
        gameUI = FindObjectOfType<GameUI>();
    }

    public void CollectCoin()
    {
        Coins++;
        SessionCoinsCollected++;
    }

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

    public void ClaimCode()
    {
        CodeUsed = true;
        Coins += 100000;
    }

    private void Awake()
    {
        ManageSingleton();

        MaxPlayerHealth = ComputeHealth();
        MaxPlayerMana = ComputeManaCapacity();

        HealthLevel = 1;
        SpellLevel = 1;
        ManaLevel = 1;

        Score = 0;
        HighScore = 0;
        NewHighScore = false;
        ScoreModifier = 1;

        Difficulty = 1;
        HighestDifficultyReached = 0;

        Coins = 0;
        SessionCoinsCollected = 0;
        CodeUsed = false;

        gameUI = FindObjectOfType<GameUI>();
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
