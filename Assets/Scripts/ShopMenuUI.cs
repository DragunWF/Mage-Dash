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
    private CosmeticManager cosmeticManager;
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
        cosmeticManager = FindObjectOfType<CosmeticManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();

        coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();
        promptText = GameObject.Find("PromptText").GetComponent<TextMeshProUGUI>();

        string[] stats = { "health", "mana", "spell" };
        const int basePrice = 15;
        for (int i = 0; i < stats.Length; i++)
        {
            string textName = string.Format("{0}Text", Utils.Capitalize(stats[i]));
            shopItems.Add(stats[i], GameObject.Find(textName).GetComponent<TextMeshProUGUI>());
            prices.Add(stats[i], basePrice - (i * 5)); // Price varies by an interval of 5 each
        }
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
