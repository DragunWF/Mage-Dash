using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CosmeticManager : MonoBehaviour
{
    public RuntimeAnimatorController EquippedCosmetic { get; private set; }

    private Dictionary<string, bool> equippedCosmetics = new Dictionary<string, bool>();
    private Dictionary<string, RuntimeAnimatorController> cosmetics = new Dictionary<string, RuntimeAnimatorController>();
    private static CosmeticManager instance;

    public void ChangeCosmetic(string cosmeticName)
    {
        EquippedCosmetic = cosmetics[cosmeticName];
        equippedCosmetics[cosmeticName] = true;
    }

    private void Awake()
    {
        ManageSingleton();

        string[] cosmeticNames = { "mage", "ghost", "arthur" };
        foreach (string name in cosmeticNames)
        {
            equippedCosmetics.Add(name, name == "mage"); // Set default outfit as true
            string formattedPath = string.Format("Animations/Player{0}", Utils.Capitalize(name));
            cosmetics.Add(name, Resources.Load(formattedPath) as RuntimeAnimatorController);
        }
    }

    private void Start()
    {
        EquippedCosmetic = cosmetics["mage"]; // default cosmetic
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
