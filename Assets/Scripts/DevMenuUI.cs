using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class DevMenuUI : MonoBehaviour
{
    private const string devCode = "dragunwf";
    private GameStats gameStats;

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
            }
            else
            {
                promptText.text = "Code already used!";
                promptText.color = Utils.GetErrorColor();
            }
        }
        else
        {
            promptText.text = "Invalid code!";
            promptText.color = Utils.GetErrorColor();
        }
    }

    private void Awake()
    {
        codeField = GameObject.Find("CodeField").GetComponent<TMP_InputField>();
        coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();
        promptText = GameObject.Find("PromptText").GetComponent<TextMeshProUGUI>();
        gameStats = FindObjectOfType<GameStats>();
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
}
