using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public sealed class LeaderboardUI : MonoBehaviour
{
    private Dictionary<string, int> players = new Dictionary<string, int>();
    private List<string> playerNames = new List<string>();
    private List<int> playerScores = new List<int>();
    private GameStats gameStats;

    private void Awake()
    {
        gameStats = FindObjectOfType<GameStats>();

        players.Add("You", gameStats.HighScore);
        players.Add("Warcook", 3000);
        players.Add("CPTInvincible36", 2500);
        players.Add("AuroraMortis", 2000);
        players.Add("Extalia", 1500);

        foreach (KeyValuePair<string, int> pair in players)
        {
            playerNames.Add(pair.Key);
            playerScores.Add(pair.Value);
        }

        playerScores.Sort();
        playerScores.Reverse();
    }

    private void Start()
    {
        // for (int i = 1; i <= playerNames.Count; i++)
        // {
        //     string textName = string.Format("Player{0}Text", i);
        //     TextMeshProUGUI textObj = GameObject.Find(textName).GetComponent<TextMeshProUGUI>();

        // }
    }

    private string FindPlayer(int score)
    {
        return "";
    }
}
