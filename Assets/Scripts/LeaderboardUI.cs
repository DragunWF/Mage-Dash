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
        players.Add("CPTInvincible", 2500);
        players.Add("AuroraMortis", 2000);
        players.Add("Extalia", 1500);

        foreach (int score in players.Values)
        {
            playerScores.Add(score);
        }

        playerScores.Sort();
        playerScores.Reverse();
    }

    private void Start()
    {
        for (int i = 0; i < playerScores.Count; i++)
        {
            string textName = string.Format("{0}_PlayerText", i + 1);
            TextMeshProUGUI textObj = GameObject.Find(textName).GetComponent<TextMeshProUGUI>();
            string playerName = FindPlayer(playerScores[i]), formattedScore = Utils.FormatNumber(playerScores[i]);
            textObj.text = string.Format("{0} - {1}: {2}", OrdinalPlace(i + 1), playerName, formattedScore);
        }
    }

    private string FindPlayer(int score)
    {
        foreach (KeyValuePair<string, int> pair in players)
        {
            if (pair.Value == score && !playerNames.Contains(pair.Key))
            {
                playerNames.Add(pair.Key);
                return pair.Key;
            }
        }
        return null;
    }

    private string OrdinalPlace(int number)
    {
        string ordinalPlace;

        switch (number)
        {
            case 1:
                ordinalPlace = "st";
                break;
            case 2:
                ordinalPlace = "nd";
                break;
            case 3:
                ordinalPlace = "rd";
                break;
            default:
                ordinalPlace = "th";
                break;
        }

        return string.Format("{0}{1}", number, ordinalPlace);
    }
}
