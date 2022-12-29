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
    private bool lockPromptText = false;

    private GameStats gameStats;
    private CosmeticManager cosmeticManager;
    private AudioPlayer audioPlayer;

    #region Upgrade Methods

    public void UpgradeMana() => Upgrade("mana");
    public void UpgradeHealth() => Upgrade("health");
    public void UpgradeSpell() => Upgrade("spell");

    #endregion

    #region Cosmetic Methods

    public void EquipMageCosmetic() => EquipCosmetic("mage");
    public void EquipGhostCosmetic() => EquipCosmetic("ghost");
    public void EquipArthurCosmetic() => EquipCosmetic("arthur");

    #endregion

    #region Hover Methods

    public void OnSpellButtonHover() => OnButtonHover("spell");
    public void OnHealthButtonHover() => OnButtonHover("health");
    public void OnManaButtonHover() => OnButtonHover("mana");
    public void OnMageButtonHover() => OnButtonHover("mage");
    public void OnGhostButtonHover() => OnButtonHover("ghost");
    public void OnArthurButtonHover() => OnButtonHover("arthur");

    #endregion

    public void OnButtonHoverExit()
    {
        if (!lockPromptText)
        {
            promptText.text = "Hover over the upgrade buttons to view price";
            promptText.color = Utils.GetGreenColor();
        }
    }

    public void UpdateLevelText(string type)
    {
        foreach (KeyValuePair<string, TextMeshProUGUI> pair in shopItems)
        {
            if (type == pair.Key)
            {
                UpdateLevels();
                shopItems[type].text = string.Format("{0} Level: {1}", pair.Key, statLevels[type]);
                break;
            }
        }
    }

    public bool CheckAffordable(int price)
    {
        if (gameStats.Coins < price)
        {
            audioPlayer.PlayError();
            lockPromptText = true;

            promptText.color = Utils.GetErrorColor();
            promptText.text = "You don't have enough coins!";

            return false;
        }

        return true;
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
            prices.Add(stats[i], basePrice - i * 5); // Price varies by an interval of 5 each
        }

        string[] cosmetics = cosmeticManager.GetCosmeticNames();
        for (int i = 0; i < cosmetics.Length; i++)
        {
            prices.Add(cosmetics[i], cosmetics[i] != "mage" ?
                                     basePrice + (i + 1) * 5 : 0);
        }
    }

    private void Start()
    {
        UpdateCoinText();
        UpdateLevels();
    }

    private void Upgrade(string stat)
    {
        if (statLevels[stat] < gameStats.GetMaxLevel() && CheckAffordable(prices[stat]))
        {
            audioPlayer.PlaySuccess();
            gameStats.UpgradeStat(stat, prices[stat]);

            lockPromptText = false;
            UpdateCoinText();
            UpdateLevelText(stat);
        }
        else if (statLevels[stat] >= gameStats.GetMaxLevel())
        {
            audioPlayer.PlayError();
            lockPromptText = true;
            promptText.text = "It has already reached its maximum level!";
            promptText.color = Utils.GetErrorColor();
        }
    }

    private void UpdateLevels()
    {
        statLevels.Clear();
        statLevels.Add("mana", gameStats.ManaLevel);
        statLevels.Add("health", gameStats.HealthLevel);
        statLevels.Add("spell", gameStats.SpellLevel);
    }

    private void UpdateCoinText()
    {
        string formattedAmount = Utils.FormatNumber(gameStats.Coins);
        coinText.text = string.Format("Coins: {0}", formattedAmount);
    }

    private void UpdateCosmeticButtons()
    {
        string[] cosmeticNames = cosmeticManager.GetCosmeticNames();
        foreach (string name in cosmeticNames)
        {
            string textName = string.Format("{0}ButtonText", Utils.Capitalize(name));
            TextMeshProUGUI textObj = GameObject.Find(textName).GetComponent<TextMeshProUGUI>();
            textObj.text = cosmeticManager.EquippedCosmeticName == name ? "Equipped" : "Equip";
        }
    }

    private void EquipCosmetic(string cosmetic)
    {
        bool ownsCosmetic = cosmeticManager.ownedCosmetics[cosmetic];

        if (cosmeticManager.EquippedCosmeticName == cosmetic && ownsCosmetic)
        {
            audioPlayer.PlayError();
            promptText.text = "This cosmetic is already equipped";
            lockPromptText = true;
        }
        else if (CheckAffordable(prices[cosmetic]) || ownsCosmetic)
        {
            audioPlayer.PlaySuccess();
            cosmeticManager.ChangeCosmetic(cosmetic);
            UpdateCosmeticButtons();

            if (!ownsCosmetic)
            {
                promptText.text = "You now own this cosmetic";
                gameStats.SubtractCoins(prices[cosmetic]);
                UpdateCoinText();
            }
        }
    }

    private void OnButtonHover(string type)
    {
        if (lockPromptText)
        {
            promptText.color = Utils.GetGreenColor();
            lockPromptText = false;
        }

        bool isCosmetic = cosmeticManager.ownedCosmetics.ContainsKey(type);
        if (isCosmetic && cosmeticManager.ownedCosmetics[type])
        {
            promptText.text = cosmeticManager.EquippedCosmeticName == type ?
                              "You already have this cosmetic equipped" : "Click to equip this cosmetic";
        }
        else if (!isCosmetic && statLevels[type] >= gameStats.GetMaxLevel())
        {
            promptText.text = "This stat is already at max level!";
        }
        else
        {
            int price = prices[type];
            string coinWord = price > 1 ? "coins" : "coin";
            string itemType = isCosmetic ? "Cosmetic" : "Upgrade";
            promptText.text = string.Format("{0} Price: {1} {2}", itemType, price, coinWord);
        }
    }
}
