using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class DevMenuUI : MonoBehaviour
{
    private const string devCode = "dragunwf";
    private GameStats gameStats;
    private AudioPlayer audioPlayer;

    private TMP_InputField codeField;
    private TextMeshProUGUI coinText;
    private TextMeshProUGUI promptText;

    public void SubmitCode()
    {
        if (codeField.text.ToLower() == devCode)
        {
            if (!gameStats.CodeUsed)
            {
                promptText.text = "Enjoy your extra 100,000 coins!";
                promptText.color = Utils.GetGreenColor();

                audioPlayer.PlayUpgrade();
                gameStats.ClaimCode();
            }
            else
            {
                promptText.text = "Code already used!";
                OnError();
            }
        }
        else
        {
            promptText.text = "Invalid code!";
            OnError();
        }
    }

    private void Awake()
    {
        codeField = GameObject.Find("CodeField").GetComponent<TMP_InputField>();
        coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();
        promptText = GameObject.Find("PromptText").GetComponent<TextMeshProUGUI>();

        gameStats = FindObjectOfType<GameStats>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        string formattedCoins = Utils.FormatNumber(gameStats.Coins);
        coinText.text = string.Format("Coins: {0}", formattedCoins);
    }

    private void OnError()
    {
        audioPlayer.PlayError();
        promptText.color = Utils.GetErrorColor();
    }
}
