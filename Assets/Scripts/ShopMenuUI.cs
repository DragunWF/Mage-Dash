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

    private TextMeshProUGUI coinText;
    private bool initializedLevels = false;

    private GameStats gameStats;
    private AudioPlayer audioPlayer;

    #region Upgrade Methods

    public void UpgradeMana() { Upgrade("mana"); }
    public void UpgradeHealth() { Upgrade("health"); }
    public void UpgradeSpell() { Upgrade("spell"); }

    #endregion

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

        shopItems.Add("mana", GameObject.Find("ManaText").GetComponent<TextMeshProUGUI>());
        shopItems.Add("health", GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>());
        shopItems.Add("damage", GameObject.Find("DamageText").GetComponent<TextMeshProUGUI>());

        prices.Add("mana", 15);
        prices.Add("health", 15);
        prices.Add("spell", 20);
    }

    private void Start()
    {
        coinText.text = string.Format("Coin Amount: {0}", gameStats.Coins);

        foreach (KeyValuePair<string, int> pair in prices)
        {
            string formatted = string.Format("{0}Text", Capitalize(pair.Key));
            TextMeshProUGUI textObj = GameObject.Find(formatted).GetComponent<TextMeshProUGUI>();
            textObj.text = string.Format("Price: {1}", pair.Value);
        }
    }

    private void Upgrade(string stat)
    {
        audioPlayer.PlayUpgrade();
        gameStats.UpgradeStat(stat);
    }

    private void UpdateLevels()
    {
        if (!initializedLevels)
        {
            statLevels.Clear();
            statLevels.Add("mana", gameStats.ManaLevel);
            statLevels.Add("health", gameStats.HealthLevel);
            statLevels.Add("damage", gameStats.SpellLevel);
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
                    case "damage":
                        statLevels[pair.Key] = gameStats.SpellLevel;
                        break;
                }
            }
        }
    }

    private string Capitalize(string original)
    {
        char firstLetter = (char)((int)original[0] - 32);
        string remaining = original.Substring(1);
        return string.Format("{0}{1}", firstLetter, remaining);
    }
}
