using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CosmeticManager : MonoBehaviour
{
    public string EquippedCosmeticName { get; private set; }
    public RuntimeAnimatorController EquippedCosmetic { get; private set; }
    public Dictionary<string, bool> ownedCosmetics = new Dictionary<string, bool>();

    private Dictionary<string, RuntimeAnimatorController> cosmetics = new Dictionary<string, RuntimeAnimatorController>();
    private static CosmeticManager instance;

    public void ChangeCosmetic(string cosmeticName)
    {
        EquippedCosmetic = cosmetics[cosmeticName];
        EquippedCosmeticName = cosmeticName;
        ownedCosmetics[cosmeticName] = true;
    }

    public string[] GetCosmeticNames()
    {
        string[] cosmeticNames = new string[cosmetics.Count];
        int index = 0;
        foreach (string key in cosmetics.Keys)
        {
            cosmeticNames[index] = key;
            index++;
        }
        return cosmeticNames;
    }

    private void Awake()
    {
        ManageSingleton();

        string[] cosmeticNames = { "mage", "ghost", "arthur" };
        foreach (string name in cosmeticNames)
        {
            ownedCosmetics.Add(name, name == "mage"); // Set default outfit as true
            string formattedPath = string.Format("Animations/Player{0}", Utils.Capitalize(name));
            cosmetics.Add(name, Resources.Load(formattedPath) as RuntimeAnimatorController);
        }
    }

    private void Start()
    {
        EquippedCosmetic = cosmetics["mage"]; // default cosmetic
        EquippedCosmeticName = "mage";
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
