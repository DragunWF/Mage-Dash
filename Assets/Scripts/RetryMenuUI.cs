using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public sealed class RetryMenuUI : MonoBehaviour
{
    private GameManager gameManager;
    private GameStats gameStats;
    private AudioPlayer audioPlayer;

    private Dictionary<string, TextMeshProUGUI> texts = new Dictionary<string, TextMeshProUGUI>();
    private Dictionary<string, int> rawValues = new Dictionary<string, int>();
    private Dictionary<string, string> formattedValues = new Dictionary<string, string>();

    private GameObject newHighScoreText;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameStats = FindObjectOfType<GameStats>();
        audioPlayer = FindObjectOfType<AudioPlayer>();

        texts.Add("Score", GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>());
        texts.Add("High Score", GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>());
        texts.Add("Coins", GameObject.Find("CoinsText").GetComponent<TextMeshProUGUI>());
        texts.Add("Coins Collected", GameObject.Find("CoinsCollectedText").GetComponent<TextMeshProUGUI>());

        newHighScoreText = GameObject.Find("NewHighScoreText");
    }

    private void Start()
    {
        rawValues.Add("Score", gameStats.Score);
        rawValues.Add("High Score", gameStats.HighScore);
        rawValues.Add("Coins", gameStats.Coins);
        rawValues.Add("Coins Collected", gameStats.SessionCoinsCollected);

        foreach (KeyValuePair<string, int> kvp in rawValues)
        {
            string formatted = GameUI.FormatScore(kvp.Value);
            formattedValues[kvp.Key] = string.Format("{0}: {1}", kvp.Key, formatted);
        }

        foreach (KeyValuePair<string, TextMeshProUGUI> kvp in texts)
        {
            kvp.Value.text = formattedValues[kvp.Key];
        }

        newHighScoreText.SetActive(gameStats.NewHighScore);
    }
}
