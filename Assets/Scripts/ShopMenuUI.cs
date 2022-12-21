using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class ShopMenuUI : MonoBehaviour
{
    private Dictionary<string, int> prices = new Dictionary<string, int>();
    private Dictionary<string, int> statLevels = new Dictionary<string, int>();
    private Dictionary<string, TextMeshProUGUI> shopItems = new Dictionary<string, TextMeshProUGUI>();

    private TextMeshProUGUI promptText;
    private TextMeshProUGUI coinText;
    private Color32 greenTextColor = new Color32(114, 224, 179, 255);
    private Color32 errorTextColor = new Color32(229, 52, 38, 255);

    private bool initializedLevels = false;
    private bool lockPromptText = false;

    private GameStats gameStats;
    private AudioPlayer audioPlayer;

    #region Upgrade Methods

    public void UpgradeMana() { Upgrade("mana"); }
    public void UpgradeHealth() { Upgrade("health"); }
    public void UpgradeSpell() { Upgrade("spell"); }

    #endregion

    #region Hover Methods

    public void OnSpellButtonHover() { OnButtonHover("spell"); }
    public void OnHealthButtonHover() { OnButtonHover("health"); }
    public void OnManaButtonHover() { OnButtonHover("mana"); }

    #endregion

    public void OnButtonHoverExit()
    {
        if (!lockPromptText)
        {
            promptText.text = "Hover over the upgrade buttons to view price";
            promptText.color = greenTextColor;
        }
    }

    public void UpdateLevelText(string type)
    {
        foreach (KeyValuePair<string, TextMeshProUGUI> pair in shopItems)
        {
            if (type == pair.Key)
            {
                UpdateLevels();
                shopItems[type].text = string.Format("{0} {1}", pair.Key, statLevels[type]);
                break;
            }
        }
    }

    private void Awake()
    {
        gameStats = FindObjectOfType<GameStats>();
        audioPlayer = FindObjectOfType<AudioPlayer>();

        coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();
        promptText = GameObject.Find("PromptText").GetComponent<TextMeshProUGUI>();

        shopItems.Add("mana", GameObject.Find("ManaText").GetComponent<TextMeshProUGUI>());
        shopItems.Add("health", GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>());
        shopItems.Add("spell", GameObject.Find("SpellText").GetComponent<TextMeshProUGUI>());

        prices.Add("mana", 10);
        prices.Add("health", 15);
        prices.Add("spell", 5);
    }

    private void Start()
    {
        UpdateCoinText();
    }

    private void Upgrade(string stat)
    {
        if (gameStats.Coins > prices[stat])
        {
            audioPlayer.PlayUpgrade();
            gameStats.UpgradeStat(stat, prices[stat]);

            lockPromptText = false;
            UpdateCoinText();
        }
        else
        {
            audioPlayer.PlayError();
            lockPromptText = true;

            promptText.color = errorTextColor;
            promptText.text = "You don't have enough coins!";
        }
    }

    private void UpdateLevels()
    {
        if (!initializedLevels)
        {
            statLevels.Clear();
            statLevels.Add("mana", gameStats.ManaLevel);
            statLevels.Add("health", gameStats.HealthLevel);
            statLevels.Add("spell", gameStats.SpellLevel);
            initializedLevels = true;
        }
        else
        {
            foreach (KeyValuePair<string, int> pair in statLevels)
            {
                switch (pair.Key)
                {
                    case "mana":
                        statLevels[pair.Key] = gameStats.ManaLevel;
                        break;
                    case "health":
                        statLevels[pair.Key] = gameStats.HealthLevel;
                        break;
                    case "spell":
                        statLevels[pair.Key] = gameStats.SpellLevel;
                        break;
                }
            }
        }
    }

    private void OnButtonHover(string buttonType)
    {
        if (lockPromptText)
        {
            promptText.color = greenTextColor;
            lockPromptText = false;
        }

        int price = prices[buttonType];
        string coinWord = price > 1 ? "coins" : "coin";
        promptText.text = string.Format("Upgrade Price: {0} {1}", price, coinWord);
    }

    private void UpdateCoinText()
    {
        coinText.text = string.Format("Coins: {0}", gameStats.Coins);
    }
}
