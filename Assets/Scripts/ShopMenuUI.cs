using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class ShopMenuUI : MonoBehaviour
{
    private Dictionary<string, int> prices = new Dictionary<string, int>();
    private Dictionary<string, TextMeshProUGUI> shopItems = new Dictionary<string, TextMeshProUGUI>();
    private TextMeshProUGUI coinText;

    private GameStats gameStats;
    private AudioPlayer audioPlayer;

    #region Upgrade Methods

    public void UpgradeMana() { Upgrade("mana"); }
    public void UpgradeHealth() { Upgrade("health"); }
    public void UpgradeSpell() { Upgrade("spell"); }

    #endregion

    public void UpdateCoinText()
    {
        coinText.text = string.Format("Coin Amount: {0}", gameStats.Coins);
    }

    private void Awake()
    {
        gameStats = FindObjectOfType<GameStats>();
        audioPlayer = FindObjectOfType<AudioPlayer>();

        shopItems.Add("mana", GameObject.Find("ManaText").GetComponent<TextMeshProUGUI>());
        shopItems.Add("health", GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>());
        shopItems.Add("damage", GameObject.Find("DamageText").GetComponent<TextMeshProUGUI>());

        prices.Add("Mana Upgrade", 15);
        prices.Add("Health Upgrade", 15);
        prices.Add("Damage Upgrade", 20);
    }

    private void Start()
    {
        UpdateCoinText();
    }

    private void Upgrade(string stat)
    {
        audioPlayer.PlayUpgrade();
        gameStats.UpgradeStat(stat);
    }
}
